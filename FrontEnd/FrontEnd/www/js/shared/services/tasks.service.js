(function() {
    'use strict';

    angular
        .module('app.shared')
        .factory('tasksService', TasksService);

    function TasksService(config, localStorageService, $q) {

        var service = {
            getTasksAll: getTasksAll,
            getTasksByDate: getTasksByDate,
            removeTask: removeTask,
            clearAllTasks: clearAllTasks
        };

        return service;

        function getTasksAll() {
            var deferred = $q.defer();

            deferred.resolve(localStorageService.getItem(config.tasksStorageKey));

            return deferred.promise;
        }

        function createTask(task) {
            var deferred = $q.defer();

            getTasksAll().then(function(tasks) {
                tasks.push(task);
                localStorageService.setItem(
                    config.tasksStorageKey,
                    tasks
                );
                deferred.resolve(tasks);
            });

            return deferred.promise;
        }

        function removeTask(name) {
            var deferred = $q.defer();

            deferred.resolve(localStorageService.getItem(config.tasksStorageKey));

            return deferred.promise;
        }

        function clearAllTasks() {
            var deferred = $q.defer();

            localStorageService.removeItem(config.tasksStorageKey);
            deferred.resolve(localStorageService.getItem(config.tasksStorageKey));

            return deferred.promise;
        }
    }
})();