(function() {
    'use strict';

    angular
        .module('app.profile')
        .controller('ProfileController', ProfileController);

    /* @ngInject */
    function ProfileController($stateParams, $state, $scope, profileService, ProfileModel) {

        var vm = this;
        vm.profile = new ProfileModel();

        // //view model functions access

        init();

        function init() {
            profileService.getLocalProfile().then(function(profile) {
                if(profile) {
                    vm.profile = profile;
                }
            });
        }

        vm.saveProfile = function() {
            profileService.saveLocalProfile(vm.profile).then(function(profile) {
                console.log('Profile saved successfully.');
            });
        }

    }
})();