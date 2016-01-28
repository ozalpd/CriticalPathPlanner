var app = angular.module('fileUploadModule', []);

var fileUpload = {};
fileUpload.imageOnly = true;

app.controller('fileUploadCtrl', ['$scope', '$rootScope', 'uploadManager',
    function ($scope, $rootScope, uploadManager) {
        $scope.files = [];
        $scope.percentage = 0;
        //Ozalp 2014.01.07
        $scope.uploadedFiles = [];
        $scope.selected = '';

        $scope.upload = function () {
            uploadManager.upload();
            $scope.files = [];
        };

        $rootScope.$on('fileAdded', function (e, call) {
            $scope.files.push(call);
            $scope.$apply();
        });

        $rootScope.$on('uploadProgress', function (e, call) {
            $scope.percentage = call;
            $scope.$apply();
        });

        //Ozalp 2014.01.07
        $rootScope.$on('uploadDone', function (e, call) {
            $scope.selected = call;
            $scope.uploadedFiles.push(call);
            $scope.$apply();
        });

        //Ozalp 2014.01.07
        $scope.setSelected = function (item) {
            $scope.selected = item;
        }

        //Ozalp 2014.01.06
        $scope.removeFile = function (item) {
            var index = $scope.files.indexOf(item);
            uploadManager.removeFile(index);
            $scope.files = uploadManager.files();
        }
    }
]);

// Format byte numbers to readable presentations:
app.provider('formatFileSizeFilter', function () {
    var $config = {
        // Byte units following the IEC format
        // http://en.wikipedia.org/wiki/Kilobyte
        units: [
            { size: 1000000000, suffix: ' GB' },
            { size: 1000000, suffix: ' MB' },
            { size: 1000, suffix: ' KB' }
        ]
    };
    this.defaults = $config;
    this.$get = function () {
        return function (bytes) {
            if (!angular.isNumber(bytes)) {
                return '';
            }
            var unit = true,
                i = 0,
                prefix,
                suffix;
            while (unit) {
                unit = $config.units[i];
                prefix = unit.prefix || '';
                suffix = unit.suffix || '';
                if (i === $config.units.length - 1 || bytes >= unit.size) {
                    return prefix + (bytes / unit.size).toFixed(2) + suffix;
                }
                i += 1;
            }
        };
    };
});

app.factory('uploadManager', function ($rootScope) {
    var _files = [];
    return {
        add: function (file) {
            var fileObj = file.files[0];
            if (!fileUpload.imageOnly || fileObj.type == 'image/jpeg' | fileObj.type == 'image/png') {
                _files.push(file);
                $rootScope.$broadcast('fileAdded', fileObj);
            }
        },
        clear: function () {
            _files = [];
        },
        files: function () {
            var fileNames = [];
            $.each(_files, function (index, file) {
                fileNames.push(file.files[0]);
            });
            return fileNames;
        },
        upload: function () {
            $.each(_files, function (index, file) {
                file.submit();
            });
            this.clear();
        },
        setProgress: function (percentage) {
            $rootScope.$broadcast('uploadProgress', percentage);
        },
        //Ozalp 2014.01.06
        removeFile: function (index) {
            _files.splice(index, 1);
        },
        //Ozalp 2014.01.07
        setResult: function (result) {
            $rootScope.$broadcast('uploadDone', result);
        }
    };
});

app.directive('upload', ['uploadManager', function factory(uploadManager) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).fileupload({
                dataType: 'text',
                add: function (e, data) {
                    uploadManager.add(data);
                },
                progressall: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    uploadManager.setProgress(progress);
                },
                done: function (e, data) {
                    uploadManager.setProgress(0);
                    //Ozalp 2014.01.07
                    uploadManager.setResult(data.result);
                }
            });
        }
    };
}]);