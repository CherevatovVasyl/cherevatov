var DirectoryApp = angular.module('DirectoryApp', [])

DirectoryApp.controller('DirectoryController', function ($scope, DirectoryService) {

    $scope.message = "Directory";
    getDirectory();

    function getDirectory() {
        DirectoryService.getDirectory("/api/Directories/Get")
            .success(function (dir) {
                
                    $scope.Directory = dir;
                    console.log($scope.Directory);
                
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.GetFileCnt = function (subDir) {
        DirectoryService.getDirectory("/api/Directories/GetFilesCount?path=" + $scope.Directory.Name)
            .success(function (cnt) {

                $scope.Cnt = cnt;
                console.log($scope.Cnt);

            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.GetNewDir = function (subDir) {
        $scope.Cnt = null;
        if ($scope.Directory.Name == null) {
            var c = subDir
        }
        else {
            var c = $scope.Directory.Name + '\\' + subDir
        }
        
        DirectoryService.getDirectory("/api/Directories/Get?path=" + c)
            .success(function (dir) {
                if (dir.Message == "Error1") {
                    alert("Server haven't enough permission");
                }
                else
                    if (dir.Message == "Error2") {
                    alert("Drive doesn't work");
                }
                else {
                    $scope.Directory = dir;
                    console.log($scope.Directory);
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.GetPrevDir = function (dirGet) {
        $scope.Cnt = null;
        var path = dirGet.substring(0, dirGet.lastIndexOf('\\'));
        if (path[path.length - 1] == ':') path = path + "\\";
        DirectoryService.getDirectory("/api/Directories/Get?path=" + path)
            .success(function (dir) {
                if (dir.Message == "Error1") {
                    alert("Server haven't enough permission");
                }
                else {
                    $scope.Directory = dir;
                    console.log($scope.Directory);
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.GetDrives = function (dirGet) {
        $scope.Cnt = null;
        DirectoryService.getDirectory("/api/Directories/Get?path=0")
            .success(function (dir) {
                if (dir.Message == "Error1") {
                    alert("Server haven't enough permission");
                }
                else {
                    $scope.Directory = dir;
                    $scope.Directory.Subdirectories = dir.Drives;
                    $scope.Directory.Files = null;
                    console.log($scope.Directory);
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

});

DirectoryApp.factory('DirectoryService', ['$http', function ($http) {

    var DirectoryService = {};
    DirectoryService.getDirectory = function (dirServ) {
        return $http.get(dirServ);
    };
    return DirectoryService;

}]);