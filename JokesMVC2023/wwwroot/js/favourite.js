window.addEventListener('load', async (e) => {
    setFavouriteDDL();
    document.getElementById("createListForm").addEventListener('submit', async (e) => {
        await handleCreateList(e);
    })
})

async function removeJoke(e) {

    let jokeId = e.target.dataset.value
    let listID = sessionStorage.getItem("listID");

    let favListItem = {
        FavouriteListId: listID,
        JokeId: jokeId
    }

    let result = await fetch('/Favourite/RemoveJokeFromList', {
        method: 'DELETE',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(favListItem)
    });
    if (result.ok) {
        // Update Joke Table

        updateJokeFavouriteList(listID)
    }

    console.log(favListItem);
}

async function handleCreateList(e) {
    e.preventDefault();

    console.log(e.target["listName"].value);

    let result = await fetch('/Favourite/AddNewList', {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(e.target["listName"].value)
    });

    if (result.ok) {
        await setFavouriteDDL();
        $('#createListModal').modal('hide');
    }

}

async function updateJokeFavouriteList(listId) {
    let result = await fetch('/Favourite/GetJokesForList?listID=' + listId);
    let htmlResult = await result.text();
    document.getElementById('jokeContainer').innerHTML = htmlResult

    let buttons = document.querySelectorAll('input[data-value]');

    let buttonArray = Array.from(buttons)

    for (index in buttonArray) {
        //console.log(buttonArray[index])
        buttonArray[index].addEventListener('click', (e) => { removeJoke(e) })
        console.log(buttonArray[index])
    }
}

async function setFavouriteDDL() {

    let result = await fetch('/Favourite/GetFavouriteListDDL');
    let htmlResult = await result.text();
    document.getElementById('dropdownContainer').innerHTML = htmlResult

    let ddlContainer = document.getElementById('dropdownContainer');
    let ddl = ddlContainer.querySelector('select');
    ddl.addEventListener('change', async (e) => {
        handleDDLChange(e);
    })

}

async function handleDDLChange(e) {
    let selectedOption = e.target.selectedOptions[0]
    sessionStorage.setItem('listID', selectedOption.value)
    sessionStorage.setItem('listName', selectedOption.text)
    console.log(selectedOption)
    updateJokeFavouriteList(selectedOption.value)

}