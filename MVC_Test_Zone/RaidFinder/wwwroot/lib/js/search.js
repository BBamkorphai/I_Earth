$(document).ready(function () {
    // Sample dataset
    var dataset = [
        "Apple",
        "Appa",
        "Amway",
        "Banana",
        "Orange",
        "Pineapple",
        "Mango",
        "Grapes"
    ];

    // Function to filter results based on search input
    function filterResults(input) {
        var filteredResults = [];
        for (var i = 0; i < dataset.length; i++) {
            if (dataset[i].toLowerCase().startsWith(input.toLowerCase())) {
                filteredResults.push(dataset[i]);
            }
        }
        return filteredResults;
    }

    // Function to display search results
    function displayResults(results) {
        var resultsList = $('#results');
        resultsList.empty();
        for (var i = 0; i < results.length; i++) {
            resultsList.append('<li>' + results[i] + '</li>');
        }
    }

    // Event listener for search input changes
    $('#search').on('input', function () {
        var searchText = $(this).val();
        var filteredResults = filterResults(searchText);
        displayResults(filteredResults);
    });
});
