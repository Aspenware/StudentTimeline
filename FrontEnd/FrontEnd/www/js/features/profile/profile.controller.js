(function() {
    'use strict';

    angular
        .module('app.profile')
        .controller('ProfileController', ProfileController);

    /* @ngInject */
    function ProfileController($stateParams, $state, $scope) {

        var vm = this;

        init();

        function init() {
            
        }

    }
})();