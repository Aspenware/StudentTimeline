(function() {
    'use strict';

    angular
        .module('app.config')
        .constant('config', {
            appTitle: 'StudentTimeline',

            // exception handler settings
            appErrorPrefix: '[StudentTimeline Error] ',

            // logger settings
            logToConsole: true,
            logToServer: false,
            consoleLogLevel: 'info',
            serverLogLevel: 'warn',
            maxFileSize: 2097152,
            sessionStoragePrefix: '',

            
            //API urls
            baseServiceUrl: '/api',
            profilesUrl: '/profiles',
            tasksUrl: '/tasks'
        });
})();
