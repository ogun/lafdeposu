findWordsApp.factory("ListWord", ["$resource", function ($resource) {
    return {
        database: $resource("/Kelime/Listele/"),
        update  : $resource("/Kelime/Guncelle/:word?meaning=:meaning&id=:id", {}, { 'query': { method: 'GET', isArray: false } }),
        approve : $resource("/Kelime/Onayla/:word?id=:id", {}, { 'query': { method: 'GET', isArray: false } }),
        remove  : $resource("/Kelime/Sil/:word?id=:id", {}, { 'query': { method: 'GET', isArray: false } })
    }
}]);


findWordsApp.controller("wordListCtrl", ["$scope", "ListWord", function ($scope, ListWord) {

    // Güncellemek için
    $scope.updateWord = function (id) {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        var word = $("#word" + id).val();
        var meaning = $("#meaning" + id).val();

        $scope.wordResponse = ListWord.update.query({
            word: word,
            meaning: meaning,
            id: id
        });

        $scope.wordResponse.$promise["finally"](function () {
            $scope.updateList();

            $loadingContainer.addClass("hide");

            var message = {title: $scope.wordResponse.Title, message: $scope.wordResponse.Message}
            switch($scope.wordResponse.Type) {
                case 0: $.growl.error(message); break;
                case 1: $.growl.notice(message); break;
                default: $.growl.warning(message);
            }
        });
    }

    // Onaylamak için
    $scope.approveWord = function (id) {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        var word = $("#word" + id).val();

        $scope.wordResponse = ListWord.approve.query({
            word: word,
            id: id
        });

        $scope.wordResponse.$promise["finally"](function () {
            $scope.updateList();

            $loadingContainer.addClass("hide");

            var message = { title: $scope.wordResponse.Title, message: $scope.wordResponse.Message }
            switch ($scope.wordResponse.Type) {
                case 0: $.growl.error(message); break;
                case 1: $.growl.notice(message); break;
                default: $.growl.warning(message);
            }
        });
    }

    // Silmek için
    $scope.removeWord = function (id) {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        var word = $("#word" + id).val();

        $scope.wordResponse = ListWord.remove.query({
            word: word,
            id: id
        });

        $scope.wordResponse.$promise["finally"](function () {
            $scope.updateList();

            $loadingContainer.addClass("hide");

            var message = { title: $scope.wordResponse.Title, message: $scope.wordResponse.Message }
            switch ($scope.wordResponse.Type) {
                case 0: $.growl.error(message); break;
                case 1: $.growl.notice(message); break;
                default: $.growl.warning(message);
            }
        });
    }

    // Onay bekleyen kelimeler
    $scope.wordList = []

    $scope.updateList = function () {
        $scope.wordList = ListWord.database.query();
    }

    $scope.updateListOnLoad = function () {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        $scope.updateList();

        $loadingContainer.addClass("hide");
    }
    
    // Sayfa açılırken güncelleyelim
    $scope.updateListOnLoad();
}]);