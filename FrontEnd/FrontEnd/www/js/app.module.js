(function() {
	'use strict';

	angular.module('app', [
        /* angular modules */
        'ionic',

        /* custom app modules */
	    'app.core',
        'app.config',
        'app.shared',

        /* main site features */
        'app.profile',
        'app.tasks'
	]);
})();