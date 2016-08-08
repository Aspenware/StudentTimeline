var userService = (function () {
    var returnObj = {};
    var serviceUrl = "http://awstudenttimelinedev.westus.cloudapp.azure.com:8661/api/User";

    returnObj.login = function ($http, $state, $stateParams, email) {

        $http({
            method: 'GET',
            url: serviceUrl + "?$filter=substringof('" + email + "',Email)",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then(function (response) {

                if (response.data.length > 0) {
                    returnObj.currentUser = response.data[0];

                    $state.transitionTo('app.profile', $stateParams);
                }
                else
                    console.log('Login Error: User Not Found');

            }
        , function (err) {
            console.log('Login Error: ' + err);
        });
    };

    returnObj.addPhoto = function () {

        navigator.camera.getPicture(onPhotoSuccess, onPhotoFail, {
            quality: 50,
            destinationType: Camera.DestinationType.FILE_URI
        });
    };

    function onPhotoSuccess(imageURI) {
        var avatar = document.getElementById('profileImage');
        avatar.setAttribute('style', "background-image: url('" + imageURI + "'); margin-bottom: 0");
        returnObj.currentUser.profileImageUrl = imageURI;
    }

    function onPhotoFail(message) {

        alert('Failed because: ' + message);
    }

    return returnObj;
}());