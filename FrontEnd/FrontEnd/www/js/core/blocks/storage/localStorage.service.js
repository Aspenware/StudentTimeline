(function() {
    'use strict';

    angular
        .module('app.core')
        .factory('localStorageService', LocalStorageService);

    function LocalStorageService(config) {

        var service = {
            setItem: setItem,
            setItems: setItems,
            getItem: getItem,
            removeItem: removeItem,
            clear: clear,
            containsKey: containsKey
        };

        return service;

        function setItem(key, value) {
            if(typeof value === 'object') {
                localStorage.setItem(key, JSON.stringify(value));
            } else {
                localStorage.setItem(key, value);
            }
            return true;
        }

        function setItems(items) {
            angular.forEach(items, function(item) {
                setItem(item.key || item.id, item.value);
            });
        }

        function getItem(key) {
            var value;

            value = localStorage.getItem(key);
            try {
                //try to convert to object
                return JSON.parse(value);
            }
            catch (err) {}

            return value;
        }

        function removeItem(key) {
            localStorage.removeItem(key);
            return true;
        }

        function clear() {
            localStorage.clear();
        }

        function containsKey(key) {
            var i, len;

            for (i = 0, len = localStorage.length; i < len; i++) {
                if (localStorage.key(i) === key) {
                    return true;
                }
            }
            return false;
        }
    }

}());