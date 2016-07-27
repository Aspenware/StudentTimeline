(function() {
    'use strict';

    angular
        .module('app.tasks')
        .controller('TasksController', TasksController);

    /* @ngInject */
    function TasksController($stateParams, tasksService) {

        var vm = this;

        init();

        function init() {
            
        }

    }
})();