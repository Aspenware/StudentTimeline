var courseService = (function () {

    var returnObj = {};
    var serviceUrl = "http://awstudenttimelinedev.westus.cloudapp.azure.com:8661/api/Course/User/";

    returnObj.getCourses = function ($http, callback) {

        $http({
            method: 'GET',
            url: serviceUrl + userService.currentUser.id,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then(function (response) {

            returnObj.courseList = response.data;
            callback();

        }
        , function (err) {
            console.log('Get Course Error: ' + err);
            callback();
        });
    };

    return returnObj;
}());