(function() {
    'use strict';

    angular
        .module('app.tasks')
        .controller('TasksViewController', TasksViewController);

    /* @ngInject */
    function TasksViewController($stateParams, tasksService) {

        var vm = this;
        vm.taskName = $stateParams.taskName;
        vm.task = new TaskModel();
        vm.tasks = [];

        init();

        function init() {
            //if name parameter is passed in, grab the task
            if(vm.taskName) {
                tasksService.getTask(vm.taskName).then(function(task) {
                    vm.tasks = [task];
                });
            }

            //otherwise, let's assume we want a week
            tasksService.getTasksAll().then(function(tasks) {
                vm.tasks = tasks;
                //do some magic to limit to a week here
            });
        }
    }
})();