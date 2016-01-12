#include "StdAfx.h"
#include "atldlgs.h"
#include "CozyKnightMainDlg.h"
#include "CozyKnightSelectTargetDlg.h"
#include "CozyKnightModifyDlg.h"
#include "rapidjson/document.h"
#include "rapidjson/prettywriter.h"
#include "rapidjson/filereadstream.h"
#include "rapidjson/reader.h"
#include "rapidjson/encodedstream.h"
#include "rapidjson/filewritestream.h"
#include <fstream>

CozyKnightMainDlg::CozyKnightMainDlg(void)
	:CBkDialogViewImplEx<CozyKnightMainDlg>(IDR_MAIN), m_core(NULL), m_nTaskCount(0), m_nSelectCount(0)
{
    HMODULE hDllLib = ::LoadLibrary(_T("CozyKnightCore.dll"));
    if(hDllLib)
    {
        typedef IKnight* (*GetInstanceFunc)();
        GetInstanceFunc lpfnGetInstance = (GetInstanceFunc)::GetProcAddress(hDllLib, "GetInstance");
        m_core = lpfnGetInstance();
    }
}

CozyKnightMainDlg::~CozyKnightMainDlg()
{
    if(m_core != NULL)
    {
        m_core->Release();
        m_core = NULL;
    }
}

BOOL CozyKnightMainDlg::OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/)
{
    InitEditBox();
    InitComboBox();
    InitSearchList();
    InitSelectList();

    SetTimer(IDC_TIMER_UPDATE, 1000);

    return True;
}

void CozyKnightMainDlg::OnClose()
{
    KillTimer(IDC_TIMER_UPDATE);
}

void CozyKnightMainDlg::OnBtnClose()
{
	EndDialog(IDCLOSE);
}

void CozyKnightMainDlg::OnSelectTarget()
{
    if(m_core != NULL)
    {
        HANDLE hTarget = NULL;
        CozyKnightSelectTargetDlg dlg(hTarget);
        if(dlg.DoModal(m_hWnd) == IDOK && hTarget != NULL)
        {
            m_core->Attach(hTarget);
            OnDeleteAllTask();
            m_searchList.DeleteAllItems();
        }
    }
}

void CozyKnightMainDlg::OnNewTask()
{
    if(m_core != NULL)
    {
        if(m_core->CreateTask() != NULL)
        {
            CString cs;
            cs.Format(_T("<listitem height=\"32\"><text pos=\"0,0,-0,-0\">任务%d</text></listitem>"), m_nTaskCount++);
            AppendListItem(IDC_TASK_LIST_CTRL, CW2A(cs, CP_UTF8), -1, TRUE);
        }
    }
}

void CozyKnightMainDlg::OnDeleteTask()
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            DeleteListItem(IDC_TASK_LIST_CTRL, nItem);

            IKnightTask* pSelectedTask =  m_core->GetTask(nItem);
            m_core->DeleteTask(pSelectedTask);
            m_searchList.DeleteAllItems();
        }
    }
}

void CozyKnightMainDlg::OnDeleteAllTask()
{
    if(m_core != NULL)
    {
        DeleteAllListItem(IDC_TASK_LIST_CTRL);
        m_core->ClearTask();
        m_searchList.DeleteAllItems();
    }
}

void CozyKnightMainDlg::InitComboBox()
{
    if(m_comboValueType.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST | WS_VSCROLL))
    {
        m_comboValueType.AddString(_T("byte"));
        m_comboValueType.AddString(_T("2 bytes"));
        m_comboValueType.AddString(_T("4 bytes"));

        m_comboValueType.SetDlgCtrlID(12);
    }
}

void CozyKnightMainDlg::InitEditBox()
{
    if(m_edtValue.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        m_edtValue.SetDlgCtrlID(11);
    }
}

void CozyKnightMainDlg::InitSearchList()
{
    DWORD dwStyle = WS_CHILD | WS_TABSTOP | WS_VISIBLE | LVS_REPORT | LVS_SHOWSELALWAYS | LVS_SINGLESEL;

    if(m_searchList.Create(m_hWnd, NULL, NULL, dwStyle, 0, 101))
    {
        DWORD dwexStyle = m_searchList.GetExtendedListViewStyle();
        dwexStyle |= (LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);
        m_searchList.SetExtendedListViewStyle(dwexStyle);

        m_searchList.AddColumn(_T("地址"), 0);
        m_searchList.AddColumn(_T("类型"), 1);
        m_searchList.AddColumn(_T("数值"), 2);
        m_searchList.SetColumnWidth(0, 235);
        m_searchList.SetColumnWidth(1, 117);
        m_searchList.SetColumnWidth(2, 235);

        m_searchList.SetDlgCtrlID(IDC_SEARCH_LIST_CTRL);
    }
}

void CozyKnightMainDlg::InitSelectList()
{
    DWORD dwStyle = WS_CHILD | WS_TABSTOP | WS_VISIBLE | LVS_REPORT | LVS_SHOWSELALWAYS | LVS_SINGLESEL;

    if(m_selectList.Create(m_hWnd, NULL, NULL,  dwStyle, 0, 101))
    {
        DWORD dwexStyle = m_selectList.GetExtendedListViewStyle();
        dwexStyle |= (LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);
        m_selectList.SetExtendedListViewStyle(dwexStyle);

        m_selectList.AddColumn(_T("名称"), 0);
        m_selectList.AddColumn(_T("地址"), 1);
        m_selectList.AddColumn(_T("类型"), 2);
        m_selectList.AddColumn(_T("数值"), 3);
        m_selectList.AddColumn(_T("锁定"), 4);
        m_selectList.SetColumnWidth(0, 117);
        m_selectList.SetColumnWidth(1, 117);
        m_selectList.SetColumnWidth(2, 117);
        m_selectList.SetColumnWidth(3, 117);
        m_selectList.SetColumnWidth(4, 117);

        m_selectList.SetDlgCtrlID(10);
    }
}

void CozyKnightMainDlg::AppendSearchItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId)
{
    CString strBuff;

    strBuff.Format(_T("%p"), lpAddr);
    m_searchList.AddItem(nItemId, 0, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_searchList.AddItem(nItemId, 1, strBuff);

    strBuff.Format(_T("%d"), nValue);
    m_searchList.AddItem(nItemId, 2, strBuff);
}

void CozyKnightMainDlg::AppendSelectedItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId, LPCTSTR lpName/* = NULL*/)
{
    CString strBuff;

    if(lpName == NULL)
    {
        strBuff.Format(_T("地址%d"), m_nSelectCount++);
        m_selectList.AddItem(nItemId, 0, strBuff);
        m_SelectedMetaInfo.push_back(MetaInfo(strBuff, FALSE));
    }
    else
    {
        m_selectList.AddItem(nItemId, 0, lpName);
        m_SelectedMetaInfo.push_back(MetaInfo(lpName, FALSE));
    }

    strBuff.Format(_T("%p"), lpAddr);
    m_selectList.AddItem(nItemId, 1, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_selectList.AddItem(nItemId, 2, strBuff);

    strBuff.Format(_T("%d"), nValue);
    m_selectList.AddItem(nItemId, 3, strBuff);

    m_selectList.AddItem(nItemId, 4, _T("FALSE"));
}

void CozyKnightMainDlg::OnSearch()
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            IKnightTask* pTask = m_core->GetTask(nItem);
            if(pTask)
            {
                int nLenght = m_edtValue.LineLength(1);
                CString strValue;

                m_edtValue.GetLine(1, strValue.GetBuffer(nLenght), nLenght);
                int nData = ::_ttoi(strValue);
                strValue.ReleaseBuffer(nLenght);

                pTask->SearchDoubleWord(nData);

                ADDRESS_LIST addrlist = pTask->GetResultAddress();

                int i = 0;
                for(ADDRESS_LIST::iterator iter = addrlist.begin(); iter != addrlist.end(); ++iter)
                {
                    int nValue = 0;
                    m_core->ReadValue(*iter, nValue);
                    AppendSearchItem(iter->addr, iter->size, nValue, i++);
                }
            }
        }
    }
}

void CozyKnightMainDlg::OnTaskLBtnUp(int nListItem)
{
    if(nListItem < m_core->GetTaskCount())
    {
        IKnightTask* pTask = m_core->GetTask(nListItem);
        if(pTask)
        {
            m_searchList.DeleteAllItems();

            ADDRESS_LIST addrlist = pTask->GetResultAddress();

            int i = 0;
            for(ADDRESS_LIST::iterator iter = addrlist.begin(); iter != addrlist.end(); ++iter)
            {
                int nValue = 0;
                m_core->ReadValue(*iter, nValue);
                AppendSearchItem(iter->addr, iter->size, nValue, i++);
            }
        }
    }
}

LRESULT CozyKnightMainDlg::OnSearchDBListClick(LPNMHDR pnmh)
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            IKnightTask* pTask = m_core->GetTask(nItem);
            if(pTask)
            {
                ADDRESS_LIST addrList = pTask->GetResultAddress();
                if(addrList.size() > 0)
                {
                    int nIndex = m_searchList.GetSelectedIndex();
                    if(nIndex >= 0)
                    {
                        int nValue = 0;
                        m_core->ReadValue(addrList[nIndex], nValue);

                        size_t nSavedCount = m_core->GetSavedAddressCount();
                        m_core->SaveAddress(addrList[nIndex]);
                        AppendSelectedItem(addrList[nIndex].addr, addrList[nIndex].size, nValue, nSavedCount);
                    }
                }
            }
        }
    }

    return S_OK;
}

LRESULT CozyKnightMainDlg::OnSelectedDBListClick(LPNMHDR pnmh)
{
    if(m_core != NULL)
    {
        int nSelected = m_selectList.GetSelectedIndex();
        if(nSelected >= 0 && nSelected < m_core->GetSavedAddressCount())
        {
            ADDRESS_LIST addrList = m_core->GetSavedAddress();
            {
                int nValue              = 0;
                ADDRESS_INFO addrInfo   = addrList[nSelected];
                MetaInfo metaInfo       = m_SelectedMetaInfo[nSelected];

                m_core->ReadValue(addrInfo, nValue);
                CozyKnightModifyDlg dlg(metaInfo.first, addrInfo,  nValue, metaInfo.second);

                if(dlg.DoModal(m_hWnd) == IDOK)
                {
                    m_core->ModifyValue(addrInfo, nValue);
                    m_core->UpdateSavedAddress(nSelected, addrInfo);
                    m_SelectedMetaInfo[nSelected] = metaInfo;

                    UpdateSelectedItem(metaInfo.first, addrInfo.addr, addrInfo.size, nValue, nSelected, metaInfo.second);
                }
            }
        }
    }

    return S_OK;
}

void CozyKnightMainDlg::UpdateSelectedItem(
    const CString& strName, 
    LPVOID lpAddr, 
    int nSize, 
    int nValue, 
    int nItemId, 
    BOOL bChekced/* = FALSE*/,
    BOOL bUsable/* = TRUE*/)
{
    CString strBuff;

    m_selectList.SetItemText(nItemId, 0, strName);

    strBuff.Format(_T("%p"), lpAddr);
    m_selectList.SetItemText(nItemId, 1, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_selectList.SetItemText(nItemId, 2, strBuff);

    if(bUsable)
    {
        strBuff.Format(_T("%d"), nValue);
    }
    else
    {
        strBuff = _T("???");
    }
    m_selectList.SetItemText(nItemId, 3, strBuff);

    m_selectList.SetItemText(nItemId, 4, (bChekced ? _T("TRUE") : _T("FALSE")));

    std::map<LPVOID, SelectedInfo>::iterator iter = m_AutoLockList.find(lpAddr);
    if(bChekced)
    {
        BOOL bEmpty = m_AutoLockList.empty();
        if(iter == m_AutoLockList.end())
        {
            ADDRESS_INFO addr;
            addr.addr = lpAddr;
            addr.size = nSize;

            m_AutoLockList[lpAddr] = std::make_pair(addr, nValue);
            if(bEmpty)
            {
                SetTimer(IDC_TIMER_LOCK, 1000);
            }
        }
    }
    else
    {
        if(iter != m_AutoLockList.end())
        {
            m_AutoLockList.erase(iter);
            if(m_AutoLockList.empty())
            {
                KillTimer(IDC_TIMER_LOCK);
            }
        }
    }
}

void CozyKnightMainDlg::OnExport()
{
    using namespace RAPIDJSON_NAMESPACE;

    if(m_core != NULL)
    {
        Document doc;
        Document::AllocatorType& alloc = doc.GetAllocator();

        Value root;
        root.SetObject();

        Value arr;
        arr.SetArray();

        int c = 0;
        ADDRESS_LIST selectedList = m_core->GetSavedAddress();
        for(ADDRESS_LIST::iterator iter = selectedList.begin(); iter != selectedList.end(); ++iter)
        {
            Value obj;
            obj.SetObject();

            Value name;
            std::string strBuffer = CW2A(m_SelectedMetaInfo[c].first, CP_UTF8);
            name.SetString(strBuffer.c_str(), strBuffer.size(), alloc);

            int nValue = 0;
            m_core->ReadValue(*iter, nValue);

            obj.AddMember("name", name, alloc);
            obj.AddMember("addr", reinterpret_cast<uint64_t>(iter->addr), alloc);
            obj.AddMember("size", iter->size, alloc);
            obj.AddMember("value", nValue, alloc);
            obj.AddMember("check", m_SelectedMetaInfo[c].second, alloc);

            arr.PushBack(obj, alloc);
            ++c;
        }

        root.AddMember("content", arr, alloc);

        CFileDialog fileDlg(FALSE, _T("json"), _T("data"), OFN_HIDEREADONLY |OFN_OVERWRITEPROMPT, _T("*.json|*.json"), m_hWnd);
        if(fileDlg.DoModal() == IDOK)
        {
            FILE* pFile = ::_tfopen (fileDlg.m_szFileName, _T("w+"));
            if(pFile != NULL)
            {
                char writeBuffer[65536];
                FileWriteStream os(pFile, writeBuffer, sizeof(writeBuffer));
                PrettyWriter<FileWriteStream, UTF8<> > writer(os);
                root.Accept(writer);

                ::fclose(pFile);
            }
        }
    }
}

void CozyKnightMainDlg::OnImport()
{
    using namespace RAPIDJSON_NAMESPACE;

    if(m_core != NULL)
    {
        CFileDialog fileDlg(TRUE, _T("json"), _T("data"), OFN_HIDEREADONLY |OFN_OVERWRITEPROMPT, _T("*.json|*.json"), m_hWnd);
        if(fileDlg.DoModal() == IDOK)
        {
            FILE* pFile = ::_tfopen(fileDlg.m_szFileName, _T("r"));

            char readBuffer[256];
            FileReadStream bis(pFile, readBuffer, sizeof(readBuffer));
            AutoUTFInputStream<unsigned, FileReadStream> eis(bis);

            Document doc;
            doc.ParseStream<0, AutoUTF<unsigned> >(eis);
            if(doc.HasParseError())
            {
                goto EXIT0;
            }

            if(!doc.IsObject() || !doc.HasMember("content"))
            {
                goto EXIT0;
            }

            const Value& content = doc["content"];
            if(!content.IsArray())
            {
                goto EXIT0;
            }

            for(int i = 0; i < content.Size(); ++i)
            {
                const Value& obj = content[i];
                if(obj.IsObject())
                {
                    CString strName;
                    if(obj.HasMember("name"))
                    {
                        const Value& name = obj["name"];
                        if(name.IsString())
                        {
                            strName = CA2W(name.GetString(), CP_UTF8);
                        }
                    }

                    uint64_t uaddr = 0;
                    if(obj.HasMember("addr"))
                    {
                        const Value& addr = obj["addr"];
                        if(addr.IsUint64())
                        {
                            uaddr = addr.GetUint64();
                        }
                    }

                    int nValue = 0;
                    if(obj.HasMember("value"))
                    {
                        const Value& value = obj["value"];
                        if(value.IsInt())
                        {
                            nValue = value.GetInt();
                        }
                    }

                    int nSize = 0;
                    if(obj.HasMember("size"))
                    {
                        const Value& size = obj["size"];
                        if(size.IsInt())
                        {
                            nSize = size.GetInt();
                        }
                    }

                    bool bCheck = FALSE;
                    if(obj.HasMember("check"))
                    {
                        const Value& check = obj["check"];
                        if(check.IsBool())
                        {
                            nSize = check.GetBool();
                        }
                    }

                    ADDRESS_INFO addrInfo;
                    addrInfo.addr = reinterpret_cast<LPVOID>(uaddr);
                    addrInfo.size = nSize;

                    m_core->SaveAddress(addrInfo);
                    AppendSelectedItem(addrInfo.addr, addrInfo.size, nValue, i, strName);
                }
            }
EXIT0:
            ::fclose(pFile);
        }
    }
}

void CozyKnightMainDlg::OnTimer(UINT_PTR nIDEvent)
{
    if(nIDEvent == IDC_TIMER_LOCK)
    {
        AutoLockProc();
    }
    else if(nIDEvent == IDC_TIMER_UPDATE)
    {
        UpdateDataProc();
    }
}

void CozyKnightMainDlg::UpdateSearchItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId, BOOL bUsable/* = TRUE*/)
{
    CString strBuff;

    strBuff.Format(_T("%p"), lpAddr);
    m_searchList.SetItemText(nItemId, 0, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_searchList.SetItemText(nItemId, 1, strBuff);

    if(bUsable)
    {
        strBuff.Format(_T("%d"), nValue);
    }
    else
    {
        strBuff = _T("???");
    }
    m_searchList.SetItemText(nItemId, 2, strBuff);
}

void CozyKnightMainDlg::UpdateDataProc()
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            IKnightTask* pTask = m_core->GetTask(nItem);
            if(pTask)
            {
                ADDRESS_LIST addrList = pTask->GetResultAddress();

                int c = 0;
                for(ADDRESS_LIST::iterator iter = addrList.begin(); iter != addrList.end(); ++iter)
                {
                    int nValue = 0;
                    BOOL bRead = m_core->ReadValue(*iter, nValue);
                    UpdateSearchItem(iter->addr, iter->size, nValue, c++, bRead);
                }
            }
        }

        int c                       = 0;
        ADDRESS_LIST selectedList   = m_core->GetSavedAddress();

        for(ADDRESS_LIST::iterator iter = selectedList.begin(); iter != selectedList.end(); ++iter)
        {
            int nValue = 0;
            BOOL bRead = m_core->ReadValue(*iter, nValue);
            UpdateSelectedItem(m_SelectedMetaInfo[c].first, iter->addr, iter->size, nValue, c, m_SelectedMetaInfo[c].second,bRead);
            ++c;
        }
    }
}

void CozyKnightMainDlg::AutoLockProc()
{
    if(m_core != NULL)
    {
        typedef std::map<LPVOID, SelectedInfo>::iterator DataIter;
        for(DataIter iter = m_AutoLockList.begin(); iter != m_AutoLockList.end(); ++iter)
        {
            m_core->ModifyValue(iter->second.first, iter->second.second);
        }
    }
}