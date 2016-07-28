(function() {
    var TaskModel = function() {
        this.id = null;
        this.users = [];
        this.course = {};
        this.title = null;
        this.description = null;
        this.taskType = null;
        this.dueDate = null;
    };

    var module = angular.module('app.shared');
    module.value('TaskModel', TaskModel);
})();