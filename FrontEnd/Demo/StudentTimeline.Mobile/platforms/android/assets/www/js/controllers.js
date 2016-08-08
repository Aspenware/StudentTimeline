angular.module('starter.controllers', [])

.controller('AppCtrl', function ($scope) {

    // With the new view caching in Ionic, Controllers are only called
    // when they are recreated or on app start, instead of every page change.
    // To listen for when this page is active (for example, to refresh data),
    // listen for the $ionicView.enter event:
    //$scope.$on('$ionicView.enter', function(e) {
    //});


    // Form data for the login modal
    $scope.loginData = {};
    $scope.isExpanded = false;
    $scope.hasHeaderFabLeft = false;
    $scope.hasHeaderFabRight = false;

    var navIcons = document.getElementsByClassName('ion-navicon');
    for (var i = 0; i < navIcons.length; i++) {
        navIcons.addEventListener('click', function () {
            this.classList.toggle('active');
        });
    }

    ////////////////////////////////////////
    // Layout Methods
    ////////////////////////////////////////

    $scope.hideNavBar = function () {
        document.getElementsByTagName('ion-nav-bar')[0].style.display = 'none';
    };

    $scope.showNavBar = function () {
        document.getElementsByTagName('ion-nav-bar')[0].style.display = 'block';
    };

    $scope.noHeader = function () {
        var content = document.getElementsByTagName('ion-content');
        for (var i = 0; i < content.length; i++) {
            if (content[i].classList.contains('has-header')) {
                content[i].classList.toggle('has-header');
            }
        }
    };

    $scope.setExpanded = function (bool) {
        $scope.isExpanded = bool;
    };

    $scope.setHeaderFab = function (location) {
        var hasHeaderFabLeft = false;
        var hasHeaderFabRight = false;

        switch (location) {
            case 'left':
                hasHeaderFabLeft = true;
                break;
            case 'right':
                hasHeaderFabRight = true;
                break;
        }

        $scope.hasHeaderFabLeft = hasHeaderFabLeft;
        $scope.hasHeaderFabRight = hasHeaderFabRight;
    };

    $scope.hasHeader = function () {
        var content = document.getElementsByTagName('ion-content');
        for (var i = 0; i < content.length; i++) {
            if (!content[i].classList.contains('has-header')) {
                content[i].classList.toggle('has-header');
            }
        }

    };

    $scope.hideHeader = function () {
        $scope.hideNavBar();
        $scope.noHeader();
    };

    $scope.showHeader = function () {
        $scope.showNavBar();
        $scope.hasHeader();
    };

    $scope.checkUser = function ($state, $stateParams) {
        if (userService.currentUser === undefined)
            $state.transitionTo('app.login', $stateParams);
    };

    $scope.clearFabs = function () {
        var fabs = document.getElementsByClassName('button-fab');
        if (fabs.length && fabs.length > 1) {
            fabs[0].remove();
        }
    };

    $scope.clearFabsLogin = function () {
        var fabs = document.getElementsByClassName('button-fab');
        if (fabs.length && fabs.length > 0) {
            fabs[0].remove();
        }
    };
})

.controller('LoginCtrl', function ($scope, $timeout, $state, $stateParams, $http, ionicMaterialInk) {
    $scope.$parent.clearFabsLogin();
    $timeout(function () {
        $scope.$parent.hideHeader();
    }, 0);
    ionicMaterialInk.displayEffect();

    $scope.loginVM = {
        email: "",
        password: ""
    };

    $scope.login = function () {

        if ($scope.loginVM.email.length > 0)
            userService.login($http, $state, $stateParams, $scope.loginVM.email);
    };

})

.controller('ProfileCtrl', function ($scope, $state, $stateParams, $timeout, ionicMaterialMotion, ionicMaterialInk) {

    $scope.$parent.checkUser($state, $stateParams);

    // Set Header
    $scope.$parent.showHeader();
    $scope.isExpanded = true;
    $scope.$parent.setExpanded(true);
    $scope.$parent.setHeaderFab('left');

    // Set Motion
    $timeout(function () {
        ionicMaterialMotion.slideUp({
            selector: '.slide-up'
        });
    }, 300);

    $timeout(function () {
        ionicMaterialMotion.fadeSlideInRight({
            startVelocity: 3000
        });
    }, 700);

    $timeout(function () {
        document.getElementById('fab-profile-image').classList.toggle('on');
    }, 1200);

    $timeout(function () {
        document.getElementById('fab-profile-save').classList.toggle('on');
    }, 1200);

    // Set Ink
    ionicMaterialInk.displayEffect();

    $scope.profile = userService.currentUser;

    $scope.addPhoto = function () {
        console.log('addPhoto: Start for ' + userService.currentUser.name);
        userService.addPhoto();
        console.log('addPhoto: End');
    };
})

.controller('saveProfileCtrl', function ($scope, $stateParams) {

    $scope.saveProfile = function () {
        console.log('saveProfile: Start for ' + userService.currentUser.name);
        console.log('saveProfile: End');
    };
})

.controller('tasksCtrl', function ($scope, $state, $stateParams, $http, $timeout, ionicMaterialMotion, ionicMaterialInk) {

    $scope.$parent.checkUser($state, $stateParams);

    //// Set Header
    $scope.$parent.showHeader();
    $scope.$parent.clearFabs();
    $scope.isExpanded = true;
    $scope.$parent.setExpanded(true);
    $scope.$parent.setHeaderFab('left');

    // Activate ink for controller
    ionicMaterialInk.displayEffect();

    $timeout(function () {
        ionicMaterialMotion.fadeSlideInRight({
            selector: '.animate-fade-slide-in .item',
            startVelocity: 3000
        });
    }, 700);

    taskService.getTasks($http, function () { $scope.tasks = taskService.taskList; });

})

.controller('taskCtrl', function ($scope, $stateParams) {
})

.controller('coursesCtrl', function ($scope, $state, $stateParams, $http, $timeout, ionicMaterialMotion, ionicMaterialInk) {

    $scope.$parent.checkUser($state, $stateParams);

    // Set Header
    $scope.$parent.showHeader();
    $scope.$parent.clearFabs();
    $scope.$parent.setHeaderFab('left');

    // Delay expansion
    $timeout(function () {
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
    }, 300);

    $timeout(function () {
        ionicMaterialMotion.fadeSlideInRight({
            selector: '.animate-fade-slide-in .item',
            startVelocity: 3000
        });
    }, 700);

    // Activate ink for controller
    ionicMaterialInk.displayEffect();

    courseService.getCourses($http, function() {
         $scope.courses = courseService.courseList;
    });
})

.controller('courseCtrl', function ($scope, $stateParams) {
})

.controller('friendsCtrl', function ($scope, $state, $stateParams, $timeout, ionicMaterialMotion, ionicMaterialInk) {

    $scope.$parent.checkUser($state, $stateParams);

    // Set Header
    $scope.$parent.showHeader();
    $scope.$parent.clearFabs();
    $scope.$parent.setHeaderFab('left');

    // Delay expansion
    $timeout(function () {
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
    }, 300);

    $timeout(function () {
        ionicMaterialMotion.fadeSlideInRight({
            selector: '.animate-fade-slide-in .item',
            startVelocity: 3000
        });
    }, 700);

    // Activate ink for controller
    ionicMaterialInk.displayEffect();

    $scope.friends = sampleData.friends;
})

.controller('friendCtrl', function ($scope, $stateParams) {
})

.controller('addFriendFabButton', function ($scope, $stateParams, $timeout, $state, $window) {

    $timeout(function () {
        document.getElementById('fab-friends').classList.toggle('on');
    }, 900);

    $scope.addContact = function () {

        navigator.contacts.pickContact(function (contact) {
            console.log('The following contact has been selected:' + JSON.stringify(contact));

            var newContact = {};
            newContact.Id = 9999;
            newContact.Name = contact.displayName;
            newContact.Description = 'New user';
            newContact.Photo = 'img/user.jpg';
            newContact.active = 0;

            sampleData.friends.push(newContact);

            $state.transitionTo($state.current, $stateParams, { reload: true });

        }, function (err) {
            console.log('Error: ' + err);
        });
    };

});
