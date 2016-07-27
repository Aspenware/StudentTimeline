(function() {
    'use strict';

    /**
     * App.Route
     *
     * Configures the application's core routing functionality.
     */
    angular
        .module('app')
        .run(appRun);

    /**
     * Internal function that is used to configure the application's router.
     * @param {Object} $log
     * @param {Object} $rootScope
     * @param {Object} $state
     * @param {Object} routerHelper
     * @ngInject
     */
    function appRun($ionicPlatform, $log, $rootScope, $state, routerHelper) {
        $ionicPlatform.ready(function() {
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

        var otherwise = '/tasks';

        routerHelper.configureStates(getStates(), otherwise);

        $rootScope.statePath = '';

        $rootScope.$on('$stateChangeStart', function(e, toState, toParams, fromState) {
            toParams.previous = fromState.name;
            // get new state http - if none defined, proceed to new state
            var stateData = toState.data;

            if (!angular.isDefined(stateData)) {
                return;
            }

        });
    }

    /**
     * Returns an array of core state objects.
     * @returns {Object[]}
     */
    function getStates() {
        return [
            {
                state: 'root',
                config: {
                    abstract: true,
                    templateUrl: 'templates/layout.html'
                }
            },
            {
                state: 'root.profile',
                config: {
                    url: '/profile',
                    title: 'Profile',
                    controller: 'ProfileController',
                    controllerAs: 'vm',
                    views: {
                        'mainContent': {
                            templateUrl: 'js/features/profile/profile.html'
                        }
                    }
                }
            },
            {
                state: 'root.tasks',
                config: {
                    url: '/tasks',
                    title: 'Tasks',
                    controller: 'TasksController',
                    controllerAs: 'vm',
                    views: {
                        'mainContent': {
                            templateUrl: 'js/features/tasks/tasks.html'
                        }
                    }
                }
            },
            {
                state: 'root.404',
                config: {
                    url: '/404',
                    templateUrl: 'templates/404.html',
                    title: '404'
                }
            },
            {
                state: 'root.login',
                config: {
                    controller: function(config) {
                        window.location.href = config.appURL;
                    }
                }
            }
        ];
    }
})();
