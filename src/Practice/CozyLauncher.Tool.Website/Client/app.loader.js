define([], function() {
    return function(dependencies) {
        var definition = {
            resolver: ['$q', '$rootScope', function($q, $rootScope) {
                var defered = $q.defer();
                require(dependencies, function() {
                    $rootScope.$apply(function() {
                        defered.resolve();
                    });
                });
                return defered.promise;
            }]
        };
        return definition;
    }
});
