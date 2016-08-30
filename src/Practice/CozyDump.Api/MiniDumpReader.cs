using System;
using System.IO;
using System.Runtime.InteropServices;
using static CozyDump.Api.DumpApi;

namespace CozyDump.Api
{
    public class MiniDumpReader : IDisposable
    {
        public const int AllocationGranularity = 0x10000; // 64k

        ulong _minidumpFileSize;
        // a handle to the opened dump file
        IntPtr _hFile = IntPtr.Zero;
        // a handle to the file mapping
        IntPtr _hFileMapping = IntPtr.Zero;
        // the address of the mapping in our process
        IntPtr _addrFileMapping;
        // the current offset in the dump being mapped
        ulong _mapCurrentOffset;
        // the current size of the mapping
        UInt32 _mappedCurrentSize;

        public MiniDumpReader(string dumpFileName)
        {
            _minidumpFileSize = (ulong)(new FileInfo(dumpFileName)).Length;
            _hFile = CreateFile(
                dumpFileName,
                EFileAccess.GenericRead,
                EFileShare.Read,
                lpSecurityAttributes: IntPtr.Zero,
                dwCreationDisposition: ECreationDisposition.OpenExisting,
                dwFlagsAndAttributes: EFileAttributes.Readonly,
                hTemplateFile: IntPtr.Zero
                );
            if (_hFile == INVALID_HANDLE_VALUE)
            {
                var hr = Marshal.GetHRForLastWin32Error();
                var ex = Marshal.GetExceptionForHR(hr);
                throw ex;
            }
            // we create a File Mapping, from which we can 
            // Map and unmap Views
            _hFileMapping = CreateFileMapping(
                _hFile,
                lpAttributes: 0,
                flProtect: AllocationProtect.PAGE_READONLY,
                dwMaxSizeHi: 0, // default to max size of file
                dwMaxSizeLow: 0,
                lpName: null
                );
            if (_hFileMapping == INVALID_HANDLE_VALUE)
            {
                var hr = Marshal.GetHRForLastWin32Error();
                var ex = Marshal.GetExceptionForHR(hr);
                throw ex;
            }
        }
        public MINIDUMP_DIRECTORY ReadStreamType(MINIDUMP_STREAM_TYPE strmType)
        {
            MINIDUMP_DIRECTORY dir = new MINIDUMP_DIRECTORY();
            var initloc = new MINIDUMP_LOCATION_DESCRIPTOR()
            {
                Rva = 0,
                DataSize = AllocationGranularity
            };
            if (MapStream(initloc) == IntPtr.Zero)
            {
                throw new InvalidOperationException("MapViewOfFileFailed");
            }
            var dirPtr = IntPtr.Zero;
            dir.location = initloc;
            var _strmPtr = IntPtr.Zero;
            var _strmSize = 0u;
            if (MiniDumpReadDumpStream(
                _addrFileMapping,
                strmType,
                ref dirPtr,
                ref _strmPtr,
                ref _strmSize
                ))
            {
                dir = Marshal.PtrToStructure<MINIDUMP_DIRECTORY>(dirPtr);
            }
            else
            {
                dir.streamType = strmType;
                dir.location.Rva = 0;
                dir.location.DataSize = 0;
            }
            return dir;
        }
        // map a section of the dump specified by a location into memory 
        // return the address of the section
        public IntPtr MapStream(MINIDUMP_LOCATION_DESCRIPTOR loc)
        {
            IntPtr retval = IntPtr.Zero;
            ulong newbaseOffset = (uint)(loc.Rva / AllocationGranularity) * AllocationGranularity;
            uint mapViewSize = AllocationGranularity * 4;
            var nLeftover = loc.Rva - newbaseOffset;
            var fAlreadyMapped = loc.Rva >= _mapCurrentOffset &&
                loc.Rva + loc.DataSize <
                _mapCurrentOffset + _mappedCurrentSize;
            if (!fAlreadyMapped)
            {
                //  try to reuse the same address
                var preferredAddress = _addrFileMapping;
                if (_addrFileMapping != IntPtr.Zero) // unmap prior
                {
                    var res = UnmapViewOfFile(_addrFileMapping);
                    if (!res)
                    {
                        throw new InvalidOperationException("Couldn't unmap view of file");
                    }
                }
                _addrFileMapping = IntPtr.Zero;
                uint hiPart = (uint)((newbaseOffset >> 32) & uint.MaxValue);
                uint loPart = (uint)newbaseOffset;
                if (loc.DataSize + nLeftover > mapViewSize)
                {
                    mapViewSize = (uint)(loc.DataSize + nLeftover);
                }
                if (newbaseOffset + mapViewSize >= _minidumpFileSize)
                {
                    mapViewSize = Math.Min((uint)(loc.DataSize + nLeftover), (uint)_minidumpFileSize);
                }
                while (_addrFileMapping == IntPtr.Zero)
                {
                    _addrFileMapping = MapViewOfFileEx(
                        _hFileMapping,
                        FILE_MAP_READ,
                        hiPart,
                        loPart,
                        mapViewSize,
                        preferredAddress
                        );
                    if (_addrFileMapping == IntPtr.Zero)  // failure
                    {
                        // if we spec'd a preferred address, that addr might now be in use
                        // so try again with no preferene
                        if (preferredAddress != IntPtr.Zero)
                        {
                            preferredAddress = IntPtr.Zero;
                            // loop
                        }
                        else
                        {
                            var hr = Marshal.GetHRForLastWin32Error();
                            var ex = Marshal.GetExceptionForHR(hr);
                            throw ex;
                        }
                    }
                }
                _mapCurrentOffset = newbaseOffset;
                _mappedCurrentSize = mapViewSize;
            }
            retval = _addrFileMapping +
                (int)(newbaseOffset - _mapCurrentOffset + nLeftover);
            return retval;
        }
        // get a string name from a relative address in the dump
        public string GetNameFromRva(uint rva)
        {
            var retstr = string.Empty;
            if (rva != 0)
            {
                var locDesc = new MINIDUMP_LOCATION_DESCRIPTOR()
                {
                    Rva = rva,
                    DataSize = 600
                };
                var locname = MapStream(locDesc);
                retstr = Marshal.PtrToStringBSTR(locname + 4); // ' skip length
            }
            return retstr;
        }
        public void Dispose()
        {
            if (_addrFileMapping != IntPtr.Zero)
            {
                UnmapViewOfFile(_addrFileMapping);
            }
            if (_hFileMapping != IntPtr.Zero)
            {
                CloseHandle(_hFileMapping);
            }
            if (_hFile != IntPtr.Zero)
            {
                CloseHandle(_hFile);
            }
        }
    }
}
