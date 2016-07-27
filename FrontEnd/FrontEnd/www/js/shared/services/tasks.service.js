(function() {
    'use strict';

    angular
        .module('app.shared')
        .factory('tasksService', TasksService);

    function TasksService(config, $q, $http) {

        var service = {
            getTasksAll: getTasksAll,
            getTasksByDate: getTasksByDate
        };

        return service;

        function getTasksAll(userName) {
            var deferred = $q.defer();

            dataService.get(config.baseApiUrl + config.suppliersUrl + '/all').then(
                function(response) {
                    deferred.resolve(response.data);
                }, function(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
    }
})();