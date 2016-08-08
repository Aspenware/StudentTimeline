var taskService = (function () {
    var returnObj = {};
    var serviceUrl = "http://awstudenttimelinedev.westus.cloudapp.azure.com:8661/api/Task/User/";

    returnObj.getTasks = function ($http, callback) {

        $http({
            method: 'GET',
            url: serviceUrl + userService.currentUser.id,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then(function (response) {

            returnObj.taskList = response.data;
            callback();

        }
        , function (err) {
            console.log('Get Task Error: ' + err);
            callback();
        });
    };

    return returnObj;
}());