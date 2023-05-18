window.addEventListener('load', (e) => {
    document.getElementById('txtSearch').addEventListener('keyup', (e) => {
        if (e.keyCode == 13) {
            handleSearchInput();
        }
    })
    document.getElementById('btnSearchClear').addEventListener('click', (e) => {
        handleSearchInput();
    })
    document.getElementById('addJokeToListForm').addEventListener('submit', async (e) => {
        handleAddJokeToList(e);
    })
})

async function handleSearchInput() {
    let query = document.getElementById('txtSearch').value;
    var result = await advFetch("/Joke/JokeTablePartial?query=" + query);
    var htmlResult = await result.text();
    document.getElementById('JokeTableContainer').innerHTML = htmlResult;
}

async function addToFavourites(jokeId) {

    sessionStorage.setItem('selectedJokeId', jokeId);

    $('#addJokeToListModal').modal('show');

    let result = await advFetch('/Favourite/GetFavouriteListDDL?jokeId='+jokeId);
    let resultHtml = await result.text();
    document.getElementById('ddlContainer').innerHTML = resultHtml;


}

async function handleAddJokeToList(e) {
    e.preventDefault();
    console.log('event fired')
    let jokeId = sessionStorage.getItem('selectedJokeId');
    let listID = e.target['favouriteList'].selectedOptions[0].value

    if (listID == 0) { return; }

    let favListItem = {
        FavouriteListId: listID,
        JokeId: jokeId
    }

    let result = await advFetch('/Favourite/AddJokeToList', {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(favListItem)
    });

    if (result.status == 400) {
        console.log(result)
        alert('The selected list already contains this joke')
        return;
    } else if (result.ok) {
        $('#addJokeToListModal').modal('hide');
    }
        
    

}

function showSpinner() {
    $('#spinnerModal').modal('show')
}

function hideSpinner() {
    $('#spinnerModal').modal('hide')
}

function startButtonSpinner() {
    let button = document.getElementById("submitButton");
    button.setAttribute('disabled', 'disabled')
    button.innerHTML = `
        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
        Loading....
        `;
}

function stopButtonSpinner() {
    let button = document.getElementById("submitButton");
    button.removeAttribute('disabled')
    button.innerHTML = "Create"
}

async function deleteConfirm(id) {
    if (confirm("Delete Joke with ID: " + id + "?")) {
        showSpinner()

        let validationToken = document.querySelector('input[name="__RequestVerificationToken"]').value

        let result = await advFetch('/Joke/Delete?id=' + id, {
            method: 'DELETE',
            headers: {
                "RequestVerificationToken": validationToken
            }
        });
        hideSpinner()
        updateTable();
    }
}


/*
* The method below will retrieve the rendered HTML for the Joke Table via a advFetch request
* The data within the response will be extracted as text.
* This 'text' will then replace the 'innerHTML' of the container div
*/

async function updateTable() {
    var result = await advFetch("/Joke/JokeTablePartial");
    var htmlResult = await result.text();
    document.getElementById('JokeTableContainer').innerHTML = htmlResult;
}

async function showEditModal(id) {
    let result = await advFetch('/Joke/EditForm?id=' + id);
    let htmlResult = await result.text();
    document.getElementById('modalBody').innerHTML = htmlResult;

    document.getElementsByClassName('modal-title')[0].innerHTML = 'Edit Joke'

    let form = document.querySelector("form[action='/Joke/Edit']")

    $.validator.unobtrusive.parse(form);

    form.addEventListener('submit', async (e) => { handleEditSubmit(e, id) })

    $('#genericModal').modal('show')
}

async function handleEditSubmit(e, id) {
    e.preventDefault();

    var form = $(e.target)

    if (!form.valid()) {
        return;
    }

    console.log(id)

    let jokeEdit = {
        id: id,
        jokeQuestion: e.target["JokeQuestion"].value,
        jokeAnswer: e.target["JokeAnswer"].value
    }

    let result = await advFetch('/Joke/Edit?id=' + id, {
        method: 'PUT',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(jokeEdit)
    })

    $('#genericModal').modal('hide')

    updateTable();


}

/*
* The method below follows the same pattern as the 'updatedTable' method,
* however, this also adds an event listener to the newly rendered form that allows for intercepting
* the 'submit' event and posting the form data. This 'submit' handler should be extracted into a separate
* method for readability
*/

async function showCreateModal() {
    // Fetch the required partial view
    var result = await advFetch("/Joke/Create");

    // convert the response into html
    var htmlResult = await result.text();

    // set the modal body to the returned html
    document.getElementById('modalBody').innerHTML = htmlResult;
    document.getElementsByClassName('modal-title')[0].innerHTML = 'Create Joke'

    var form = document.querySelector("form[action='/Joke/Create']")

    $.validator.unobtrusive.parse(form);

    form.addEventListener('submit', async (e) => {

        // Prevent the regular behavious of the form (stops it posting to the server)
        e.preventDefault();


        // TODO - validate the form

        let form = e.target;
        console.log(e.target)
        let formResult = $(form);

        if (!formResult.valid()) {
            return;
        }
        startButtonSpinner();

        // restructure the form data as a JSON object
        let jokeCreate = {
            JokeQuestion: e.target["JokeQuestion"].value,
            JokeAnswer: e.target["JokeAnswer"].value
        }

        let validationToken = document.querySelector('input[name="__RequestVerificationToken"]').value
        console.log(validationToken)

        // POST the JSON object (stringified) to the /Create endpoint
        let result = await advFetch('/Joke/Create', {
            method: "POST",
            headers: {
                "content-type": "application/json",
                "RequestVerificationToken": validationToken
            },
            body: JSON.stringify(jokeCreate)
        });

        if (result.status === 201) {
            $('#genericModal').modal('hide')
            updateTable();
            ShowToast('Joke Created', 2000, "success")

        } else {
            let test = await result.text();
            stopButtonSpinner();
            ShowToast(test, 2000, "error")
        }


    })

    // show the modal
    $('#genericModal').modal('show')

}

async function showDetailsModal(id) {

    let result = await advFetch('/Joke/Details?id=' + id);

    let htmlResult = await result.text();

    document.getElementById('modalBody').innerHTML = htmlResult;
    document.getElementsByClassName('modal-title')[0].innerHTML = 'Joke Details'

    $('#genericModal').modal('show')
}