
$(".js-data-fetch_players").select2({
    ajax: {
        url: "/Players/FetchPlayers",
        dataType: 'json',
        type: "GET",
        delay: 500,
        data: function (query) {
            if (query.term) {
                return { searchTerm: query.term };
            }
            return { searchTerm: "" };
        },
        processResults: function (data) {
            return { results: data };
        },
        cache: false
    },
    minimumInputLength: 0
});

$(".js-data-fetch_divisions").select2({
    ajax: {
        url: "/Division/FetchDivisions",
        dataType: 'json',
        type: "GET",
        delay: 500,
        data: function (query) {
            if (query.term) {
                return { searchTerm: query.term };
            }
            return { searchTerm: "" };
        },
        processResults: function (data) {
            return { results: data };
        },
        cache: false
    },
    minimumInputLength: 0
});

$(".js-data-fetch_teams").select2({
    ajax: {
        url: "/Team/FetchTeams",
        dataType: 'json',
        type: "GET",
        delay: 500,
        data: function (query) {
            if (query.term) {
                return { searchTerm: query.term };
            }
            return { searchTerm: "" };
        },
        processResults: function (data) {
            return { results: data };
        },
        cache: false
    },
    minimumInputLength: 0
});

$(".js-data-fetch_roles").select2({
    ajax: {
        url: "/Role/FetchRoles",
        dataType: 'json',
        type: "GET",
        delay: 500,
        data: function (query) {
            if (query.term) {
                return { searchTerm: query.term };
            }
            return { searchTerm: "" };
        },
        processResults: function (data) {
            return { results: data };
        },
        cache: false
    },
    minimumInputLength: 0
});