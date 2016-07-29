(function() {
    'use strict';

    /**
     * App.Config
     *
     * Configures the app.core module.
     */
    angular
        .module('app')
        .config(configure);

    /**
     * Internal function that is used to configure the app.core module.
     * @param {Object} $provide
     * @param {Object} config
     * @param {Object} routerHelperProvider
     * @param {Object} exceptionHandlerProvider
     * @param {Object} loggerProvider
     * @param {Object} $httpProvider
     * @ngInject
     */
    function configure($provide, config, routerHelperProvider, exceptionHandlerProvider, loggerProvider,
                       $httpProvider) {

        configureExceptionHandler();
        configureLogger();
        configureRouterHelper();
        configureHttp();

        ////////////////

        /**
         * Configures the Exception Handler application block.
         */
        function configureExceptionHandler() {
            $provide.provider('$exceptionHandler', exceptionHandlerProvider); //replace $exceptionHandler service
        }

        /**
         * Configures the Logger application block.
         */
        function configureLogger() {
        }

        /**
         * Configures the Router Helper application block.
         */
        function configureRouterHelper() {
        }

        /**
         * Configures the $http service.
         */
        function configureHttp() {

            $httpProvider.interceptors
                .push(function(config) {
                    return {
                        response: function(conf) {
                            var contentType = conf.headers('Content-Type'),
                                url = conf.config.url,
                                isServerResponse = url ? url.indexOf(config.baseServiceUrl) > -1 : false,
                                isHtml = contentType ? contentType.indexOf('text/html;') > -1 : false;
                            if (isServerResponse && isHtml) {
                                window.location.href = config.appURL;
                            }
                            return conf;
                        },
                        responseError: function(conf) {
                            var contentType = conf.headers('Content-Type'),
                                url = conf.config.url,
                                isServerResponse = url ? url.indexOf(config.baseServiceUrl) > -1 : false,
                                isHtml = contentType ? contentType.indexOf('text/html;') > -1 : false;
                            if (isServerResponse && isHtml) {
                                window.location.href = config.appURL;
                            }
                            return conf;
                        }
                    };
                }
            );
        }
    }
})();
