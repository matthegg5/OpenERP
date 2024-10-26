//array of objects
const cardArray = [
    {
        name: 'fries',
        img: 'media/memorygame/fries.png'
    },
    {
        name: 'cheeseburger',
        img: 'media/memorygame/cheeseburger.png'
    },
    {
        name: 'hotdog',
        img: 'media/memorygame/hotdog.png'
    },
    {
        name: 'ice-cream',
        img: 'media/memorygame/ice-cream.png'
    },
    {
        name: 'milkshake',
        img: 'media/memorygame/milkshake.png'
    },
    {
        name: 'pizza',
        img: 'media/memorygame/pizza.png'
    },
    {
        name: 'fries',
        img: 'media/memorygame/fries.png'
    },
    {
        name: 'cheeseburger',
        img: 'media/memorygame/cheeseburger.png'
    },
    {
        name: 'hotdog',
        img: 'media/memorygame/hotdog.png'
    },
    {
        name: 'ice-cream',
        img: 'media/memorygame/ice-cream.png'
    },
    {
        name: 'milkshake',
        img: 'media/memorygame/milkshake.png'
    },
    {
        name: 'pizza',
        img: 'media/memorygame/pizza.png'
    }
]
cardArray.sort(() => 0.5 - Math.random())

const gridDisplay = document.querySelector('#grid')
const resultDisplay = document.querySelector('#result')
let cardsChosen = [] //initialise empty array
let cardsChosenIds = []
const cardsWon = []

function createBoard () {
    for (let i = 0; i< cardArray.length; i++) {
        const card = document.createElement('img')
        card.setAttribute('src','media/memorygame/blank.png')
        card.setAttribute('data-id', i)
        card.addEventListener('click', flipCard) //only calls flipCard on click as a callback - exclude the double brackets for a function call, causes weird behavior
        gridDisplay.appendChild(card)
    }
}
createBoard()

function checkmatch() {
    const cards = document.querySelectorAll('#grid img') //all img within all elements with the id of "grid"
    const optionOneId = cardsChosenIds[0]
    const optionTwoId = cardsChosenIds[1]

    if (optionOneId == optionTwoId) {
        alert('Same card selected twice!')
        cards[optionOneId].setAttribute('src', 'media/memorygame/blank.png') //set the matched cards to white
        cards[optionTwoId].setAttribute('src', 'media/memorygame/blank.png')
    }

    else {
        //check for a match here
        if(cardsChosen[0] == cardsChosen[1])
        {
            alert('Match!')
            cards[optionOneId].setAttribute('src', 'media/memorygame/white.png') //set the matched cards to white
            cards[optionTwoId].setAttribute('src', 'media/memorygame/white.png')
            cards[optionOneId].removeEventListener('click', flipCard)
            cards[optionTwoId].removeEventListener('click', flipCard)
            cardsWon.push(cardsChosen)
    
        } else {
            cards[optionOneId].setAttribute('src', 'media/memorygame/blank.png') //set the matched cards to white
            cards[optionTwoId].setAttribute('src', 'media/memorygame/blank.png')
            alert('No match!')
        }
    }


    resultDisplay.textContent = cardsWon.length
    cardsChosen = [] //reset array
    cardsChosenIds = [] //reset array

    if (cardsWon.length == cardArray.length / 2) //pairs, so half number of array items
    {
        resultDisplay.innerHTML = 'Congratulations, you have matched all the pairs'
    }

}

function flipCard() {
    let cardId = this.getAttribute('data-id') //this keyworld relates to whatever element is firing the event.
    //console.log('clicked', cardId)
    cardsChosen.push(cardArray[cardId].name)
    cardsChosenIds.push(cardId)
    this.setAttribute('src', cardArray[cardId].img) //assign the "image" array value to the "src" attribute on the element
    if (cardsChosen.length === 2) {
        setTimeout(checkmatch, 500)
    }
}