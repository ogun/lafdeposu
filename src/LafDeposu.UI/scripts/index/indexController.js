﻿var findWordsApp = angular.module("findWordsApp", ["ngResource"]);

findWordsApp.factory("FindWord", ["$resource", function ($resource) {
    return {
        database: $resource("FindWord/:chars?startsWith=:startsWith&contains=:contains&endsWith=:endsWith&showTwoChars=:showTwoChars")
    }
}]);

findWordsApp.factory("Share", function () {
    createLink = function (chars, startsWith, contains, endsWith, showTwoChars) {
        var returnValue = {};
        returnValue.queryString = "";

        if (chars != null && chars != "") {
            returnValue.queryString = "keyword=" + chars;

            if (startsWith != null && startsWith != "") {
                returnValue.queryString += "&startsWith=" + startsWith;
            }

            if (contains != null && contains != "") {
                returnValue.queryString += "&contains=" + contains;
            }

            if (endsWith != null && endsWith != "") {
                returnValue.queryString += "&endsWith=" + endsWith;
            }

            if (showTwoChars != null && showTwoChars != "") {
                returnValue.queryString += "&showTwoChars=" + showTwoChars;
            }
        }

        return returnValue;
    }

    return {
        createLink: createLink
    }
});

findWordsApp.controller("wordListCtrl", ["$scope", "FindWord", "Share", function ($scope, FindWord, Share) {

    $scope.findWordsClick = function () {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        $scope.wordList = FindWord.database.query({
            chars: $scope.chars,
            startsWith: $scope.startsWith,
            contains: $scope.contains,
            endsWith: $scope.endsWith,
            showTwoChars: $scope.showTwoChars
        });

        $scope.wordList.$promise["finally"](function () {
            $loadingContainer.addClass("hide");

            $scope.share = Share.createLink($scope.chars, $scope.startsWith, $scope.contains, $scope.endsWith, $scope.showTwoChars);
        });
    }
}]);