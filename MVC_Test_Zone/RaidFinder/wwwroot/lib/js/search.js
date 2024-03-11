$(document).ready(function () {

    function displayResults(results) {
        var resultsList = $('#results');    
        resultsList.empty();
        if (results && results.length > 0) {
            for (var i = 0; i < results.length; i++) {
                resultsList.append('<li>' + results[i].name + '</li>');
            }
        } else {
            resultsList.append('<li>No results found</li>');
        }
        
    }

    $('#search').on('input', function () {
        var query = $(this).val().trim();
        if (query === '') {
            $('#results').empty();
            return;
        }
        if (query != null) {
            $.ajax({
                url: searchUrl,
                type: 'POST',
                data: { query: query },
                success: function (data) {
                    displayResults(data);
                }
            });
        }
    });
});
