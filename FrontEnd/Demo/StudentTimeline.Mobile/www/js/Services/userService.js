var userService = (function () {
    var returnObj = {};
    var serviceUrl = "";
    
    returnObj.login = function ($state) {

        navigator.contacts.pickContact(function (contact) {
            console.log('The following contact has been selected:' + JSON.stringify(contact));


            $state.transitionTo($state.current, $stateParams, {
                reload: true,
                inherit: false,
                notify: true
            });



        }, function (err) {
            console.log('Error: ' + err);
        });
    };
    
    return returnObj;
}());