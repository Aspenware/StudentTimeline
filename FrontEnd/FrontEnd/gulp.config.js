module.exports = function() {
	var client = './www/',
		clientApp = client + 'js/',
		wiredep = require('wiredep'),
		bowerFiles = wiredep({ devDependencies: true }).js,
		temp = './.tmp/',
		bower = {
			json: require('./bower.json'),
			directory: client + 'lib/',
			ignorePath: '..'
		},
		nodeModules = 'node_modules';

	var config = {
		/**
		* File paths
		*/
		// all javascript that we want to vet
		alljs: [
			'./**/*.js'
		],
		client: client,
		build: '.build/',
		css: temp + 'styles.css',
		html: client + '**/*.html',
		htmltemplates: [client + '**/*.html', '!' + client + '**/index.html'],
        images: client + 'img/**/*.*',
		index: client + 'index.html',
		js: [
			clientApp + '**/*.module.js',
			clientApp + '**/*.js'
		],
		jsOrder: [
			'**/app.module.js',
			'**/*.module.js',
			'**/*.js'
		],
		sass: client + 'css/styles.sass',
        temp: temp,

		/**
		* optimized files
		*/
		optimized: {
			app: 'app.js',
			lib: 'lib.js'
		},

		/**
		* browser sync
		*/
		browserReloadDelay: 1000,

		/**
		* template cache
		*/
		templateCache: {
			file: 'templates.js',
			options: {
				module: 'app',
				root: 'www/',
				standalone: false
			}
		},

		/**
		* Bower and NPM files
		*/
		bower: bower,
		packages: [
			'./package.json',
			'./bower.json'
		]
	};

  /**
   * wiredep and bower settings
   */
	config.getWiredepDefaultOptions = function() {
		return {
			bowerJson: config.bower.json,
			directory: config.bower.directory,
			ignorePath: config.bower.ignorePath
		};
	};

	return config;
};