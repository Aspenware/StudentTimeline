(function() {
	'use strict';

	angular
		.module('app.core', [
			'ionic',
			'blocks.exception', 'blocks.logger', 'blocks.router',
			'ui.router'
		]);
})();