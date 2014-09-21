findWordsApp.factory("AddWord", ["$resource", function ($resource) {
    return {
        database: $resource("/Kelime/Ekle/:word?meaning=:meaning", {}, { 'query': { method: 'GET', isArray: false } })
    }
}]);


findWordsApp.controller("wordAddCtrl", ["$scope", "AddWord", function ($scope, AddWord) {

    $scope.addWordClick = function () {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");


        $scope.wordResponse = AddWord.database.query({
            word: $scope.word,
            meaning: $scope.meaning
        });

        $scope.wordResponse.$promise["finally"](function () {
            $loadingContainer.addClass("hide");

            var message = {title: $scope.wordResponse.Title, message: $scope.wordResponse.Message}
            switch($scope.wordResponse.Type) {
                case 0: $.growl.error(message); break;
                case 1: $.growl.notice(message); break;
                default: $.growl.warning(message);
            }
        });
    }

}]);