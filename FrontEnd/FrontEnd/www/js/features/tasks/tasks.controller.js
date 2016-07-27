(function() {
    'use strict';

    angular
        .module('app.tasks')
        .controller('TasksController', TasksController);

    /* @ngInject */
    function TasksController($stateParams, $state, $scope) {

        var vm = this;

        init();

        function init() {
            
        }

    }
})();