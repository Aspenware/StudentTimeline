(function() {
    'use strict';

    /**
     * Configures the home module's routes.
     */
    angular
        .module('app.tasks')
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
                state: 'root.tasks',
                config: {
                    url: '/tasks',
                    title: 'Tasks',
                    views: {
                        'mainContent': {
                            templateUrl: 'js/features/tasks/tasks.html'
                        }
                    },
                    abstract: true
                }
            },
            {
                state: 'root.tasks.day',
                config: {
                    url: '/day',
                    title: 'Daily Tasks',
                    views: {
                        'tab-dayTasks': {
                            templateUrl: 'js/features/tasks/tasksDay.html'
                        }
                    }
                }
            },
            {
                state: 'root.tasks.week',
                config: {
                    url: '/week',
                    title: 'Weekly Tasks',
                    views: {
                        'tab-weekTasks': {
                            templateUrl: 'js/features/tasks/tasksWeek.html'
                        }
                    }
                }
            },
            {
                state: 'root.tasks.new',
                config: {
                    url: '/new',
                    title: 'New Task',
                    views: {
                        'tab-newTask': {
                            templateUrl: 'js/features/tasks/tasksEdit.html',
                            controller: 'TasksEditController',
                            controllerAs: 'vm'
                        }
                    }
                }
            }
        ];
    }
})();
