(function() {
    'use strict';

    angular
        .module('app.tasks')
        .controller('TasksEditController', TasksEditController);

    /* @ngInject */
    function TasksEditController(tasksService) {

        //var vm = this;
        //vm.taskName = $stateParams.taskName;
        //vm.task = new TaskModel();

        //viewmodel functions
        //vm.saveTask = saveTask;

        // init();

        // function init() {
        //     //if name parameter is passed in, grab the task
        //     // if(vm.taskName) {
        //     //     tasksService.getTask(vm.taskName).then(function(task) {
        //     //         vm.task = task;
        //     //     });
        //     // }

        //     //otherwise, let's assume 
        // }

        // vm.saveTask = function() {
        //     // tasksService.createTask(vm.task).then(function(results) {
        //     //     $state.go('root.tasks.week');
        //     // });

        // }

    }
})();