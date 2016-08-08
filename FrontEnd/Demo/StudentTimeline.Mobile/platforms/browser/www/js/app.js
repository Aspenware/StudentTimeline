// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ionic-material', 'starter.controllers', 'ionMdInput'])

.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider) {

    // Turn off caching for demo simplicity's sake
    $ionicConfigProvider.views.maxCache(0);

    $stateProvider

    .state('app', {
        url: '/app',
        abstract: true,
        templateUrl: 'templates/menu.html',
        controller: 'AppCtrl'
    })

    .state('app.login', {
        url: '/login',
        views: {
            'menuContent': {
                templateUrl: 'templates/login.html',
                controller: 'LoginCtrl'
            }
        }
    })

    .state('app.profile', {
        url: '/profile',
        views: {
            'menuContent': {
                templateUrl: 'templates/profile.html',
                controller: 'ProfileCtrl'
            },
            'fabContent': {
                template: '<button id="fab-profile-save" ng-click="saveProfile();" class="button button-fab button-fab-top-left expanded button-energized-900 spin"><i class="icon material-icons">save</i></button>',
                controller: 'saveProfileCtrl'
            }
        }
    })

    .state('app.courses', {
        url: '/courses',
        views: {
            'menuContent': {
                templateUrl: 'templates/courses.html',
                controller: 'coursesCtrl'
            },
            'fabContent': {
                template: '<button id="fab-courses" onClick="utilities.addPhoto();" class="button button-fab button-fab-top-left expanded button-energized-900 spin"><i class="icon ion-plus-round"></i></button>',
                controller: function ($timeout) {
                    $timeout(function () {
                        document.getElementById('fab-courses').classList.toggle('on');
                    }, 900);
                }
            }
        }
    })

    .state('app.course', {
        url: '/courses/:courseId',
        views: {
            'menuContent': {
                templateUrl: 'templates/course.html',
                controller: 'courseCtrl'
            }
        }
    })

    .state('app.tasks', {
        url: '/tasks',
        views: {
            'menuContent': {
                templateUrl: 'templates/tasks.html',
                controller: 'tasksCtrl'
            },
            'fabContent': {
                template: '<button id="fab-tasks" class="button button-fab button-fab-top-left expanded button-energized-900 spin"><i class="icon ion-plus-round"></i></button>',
                controller: function ($timeout) {
                    $timeout(function () {
                        document.getElementById('fab-tasks').classList.toggle('on');
                    }, 900);
                }
            }
        }
    })

    .state('app.task', {
        url: '/tasks/:Id',
        views: {
            'menuContent': {
                templateUrl: 'templates/task.html',
                controller: 'taskCtrl'
            }
        }
    })

    .state('app.friends', {
        url: '/friends',
        views: {
            'menuContent': {
                templateUrl: 'templates/friends.html',
                controller: 'friendsCtrl'
            },
            'fabContent': {
                template: '<button id="fab-friends" ng-click="addContact()" class="button button-fab button-fab-top-left expanded button-energized-900 spin"><i class="icon ion-plus-round"></i></button>',
                controller: 'addFriendFabButton'
            }
        }
    })

    .state('app.friend', {
        url: '/friends/:friendId',
        views: {
            'menuContent': {
                templateUrl: 'templates/friend.html',
                controller: 'friendCtrl'
            }
        }
    });
    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/app/login');
});
