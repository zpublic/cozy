import {Plugin, Cordova} from 'ionic-native';
import {File} from 'ionic-native';
import {Injectable} from '@angular/core'

declare var window;

@Injectable()
export class FileService {
  /**
  * Write a Existing file as string.
  *
  * @param {string} path  Base FileSystem. Please refer to the iOS and Android filesystems above
  * @param {string} fileName Name of file to remove
  * @param {string} text content of file to write
  * @return Returns a Promise that resolves or rejects with an error.
  */
  writeExistingFile(path: string, fileName: string, text: string): Promise<any> {
    let resolveFn, rejectFn;
    let promise = new Promise((resolve, reject) => { resolveFn = resolve; rejectFn = reject; });

    if ((/^\//.test(fileName))) {
      rejectFn('file-name cannot start with \/');
    }

    try {
      window.resolveLocalFileSystemURL(path, function (fileSystem) {
        fileSystem.getFile(fileName, { create: false }, function (fileEntry) {
          fileEntry.createWriter(function (writer) {

            writer.onwriteend = function () {
              if (this.error !== undefined && this.error !== null) {
                rejectFn({ code: null, message: 'READER_ONWRITEEND_ERR' });
              }
              else {
                resolveFn(fileEntry);
              }
            };

            let data = new Blob([text], { 'type': 'text/plain' });
            writer.write(data);

          }, function (error) {
            error.message = File.cordovaFileError[error.code];
            rejectFn(error);
          });
        }, function (err) {
          err.message = File.cordovaFileError[err.code];
          rejectFn(err);
        });
      }, function (er) {
        er.message = File.cordovaFileError[er.code];
        rejectFn(er);
      });

    } catch (e) {
      e.message = File.cordovaFileError[e.code];
      rejectFn(e);
    }

    return promise;
  }
} 