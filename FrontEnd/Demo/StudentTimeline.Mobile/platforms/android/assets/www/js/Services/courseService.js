var courseService = (function () {

    var returnObj = {};
    var serviceUrl = "http://10.0.0.39:8661/api/Course/User/";

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