(function() {
    'use strict';

    angular
        .module('app.shared')
        .factory('profileService', ProfileService);

    function ProfileService(config, localStorageService, $q) {

        var service = {
            getLocalProfile: getLocalProfile,
            saveLocalProfile: saveLocalProfile
        };

        return service;

        function getLocalProfile() {
            var deferred = $q.defer();

            deferred.resolve(localStorageService.getItem(config.profileStorageKey));

            return deferred.promise;
        }

        function saveLocalProfile(profile) {
            var deferred = $q.defer();

            localStorageService.setItem(
                config.profileStorageKey,
                profile
            );
            deferred.resolve(profile);

            return deferred.promise;
        }
    }
})();