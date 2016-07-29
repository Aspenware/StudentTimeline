(function() {
    var ProfileModel = function() {
        this.id = null;
        this.name = null;
        this.email = null;
        this.blurb = null;
        this.profileImageUrl = null;
    };

    var module = angular.module('app.shared');
    module.value('ProfileModel', ProfileModel);
})();