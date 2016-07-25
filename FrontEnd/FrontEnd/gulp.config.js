module.exports = function() {
	var client = './www/',
		clientApp = client + 'js',
		wiredep = require('wiredep'),
		bowerFiles = wiredep({ devDependencies: true })['js'],
		temp = './.tmp/',
		bower = {
			directory: client + 'lib/',
			ignorePath: '../..'
		},
		nodeModules = 'node_modules';

	var config = {
		/**
		* File paths
		*/
		// all javascript that we want to vet
		alljs: [
			'./www/**/*.js',
			'./*.js'
		],
		client: client,
		build: client + 'build/',
		css: temp + 'styles.css',
		html: client + '**/*.html',
		htmltemplates: clientApp + '**/*.html',
		images: client + 'img/**/*.*',
		index: client + 'index.html',
		// app js, with no specs
		js: [
			clientApp + '**/*.module.js',
			clientApp + '**/*.js',
			'!' + clientApp + '**/*.spec.js'
		],
		jsOrder: [
			'**/app.module.js',
			'**/*.module.js',
			'**/*.js'
		],
		sass: client + 'css/styles.sass',

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
				module: 'app.core',
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
		],

		/**
		* Node settings
		*/
		// nodeServer: server + 'app.js',
		// defaultPort: '8001'
	};

  /**
   * wiredep and bower settings
   */
	config.getWiredepDefaultOptions = function() {
		var options = {
			bowerJson: config.bower.json,
			directory: config.bower.directory,
			ignorePath: config.bower.ignorePath
		};
		return options;
	};

	return config;
};