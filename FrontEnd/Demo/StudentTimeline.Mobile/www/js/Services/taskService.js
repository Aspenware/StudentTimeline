var utilities = (function () {
    var returnObj = {};
    
    returnObj.addContact = function ($state) {

        navigator.contacts.pickContact(function (contact) {
            console.log('The following contact has been selected:' + JSON.stringify(contact));

            var newContact = {};
            newContact.Id = 9999;
            newContact.Name = contact.displayName;
            newContact.Description = 'New user';
            newContact.Photo = 'img/user.jpg';
            newContact.active = 0;

            sampleData.friends.push(newContact);

            $state.transitionTo($state.current, $stateParams, {
                reload: true,
                inherit: false,
                notify: true
            });



        }, function (err) {
            console.log('Error: ' + err);
        });
    };
    
    returnObj.addPhoto = function () {

        navigator.camera.getPicture(onPhotoSuccess, onPhotoFail, {
            quality: 50,
            destinationType: Camera.DestinationType.FILE_URI
        });
    };

    function onPhotoSuccess(imageURI) {

        var image = document.getElementById('myImage');
        image.src = imageURI;
    }

    function onPhotoFail(message) {

        alert('Failed because: ' + message);
    }

    return returnObj;
}());