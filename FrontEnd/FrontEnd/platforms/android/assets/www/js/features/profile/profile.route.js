(function() {
    'use strict';

    /**
     * Configures the home module's routes.
     */
    angular
        .module('app.profile')
        .run(appRun);

    /**
     * Internal function that is used to configure routes.
     * @ngInject
     */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    /**
     * Returns an array of state objects for this module.
     */
    function getStates() {
        return [
            {
                state: 'root.profile',
                config: {
                    url: '/profile',
                    title: 'Profile',
                    views: {
                        'mainContent': {
                            templateUrl: 'js/features/profile/profile.html',
                            controller: 'ProfileController',
                            controllerAs: 'vm'
                        }
                    }
                }
            }
        ];
    }
})();