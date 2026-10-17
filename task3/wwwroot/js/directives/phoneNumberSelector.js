angular.module("clientApp").directive("phoneNumberSelector", function ($http) {
    return {
        restrict: "E",
        scope: {
            selectedId: "=",
            showAll: "@",
            getData: "="
        },
        template: `
            <select class="form-control"
                    ng-model="selectedId"
                    ng-options="p.id as p.number for p in items">
                <option value="" ng-if="showAll === 'true'">All Numbers</option>
            </select>
        `,
        link: function (scope) {
            scope.items = [];

            $http.get("/api/PhoneNumbersApi").then(function (r) {
                scope.items = r.data;
            });

            scope.getData = function () {
                return scope.selectedId;
            };
        }
    };
});