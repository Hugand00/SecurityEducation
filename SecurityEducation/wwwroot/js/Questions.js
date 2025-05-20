let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function plusSlides(n) {
    // Om vi går framåt (n > 0), kontrollera om ett alternativ är valt
    if (n > 0) {
        const currentSlide = document.getElementsByClassName("mySlides")[slideIndex - 1];
        const selected = currentSlide.querySelector('input[type="radio"]:checked');
        if (!selected) {
            alert("Du måste välja ett svar innan du går vidare.");
            return; // Stoppa funktionen om inget är valt
        }
    }

    showSlides(slideIndex += n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    let prevBtn = document.querySelector(".prev");
    let nextBtn = document.querySelector(".next");

    // Begränsa slideIndex till inom bounds
    if (n > slides.length) { slideIndex = slides.length; }
    else if (n < 1) { slideIndex = 1; }
    else { slideIndex = n; }

    // Dölj alla slides
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    // Ta bort "active" från alla prickar
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    // Visa rätt slide och markera rätt dot
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";

    // Visa/dölj knappar beroende på slideIndex
    if (slideIndex === 1) {
        prevBtn.style.visibility = "hidden";
        nextBtn.style.display = "inline-block";
    } else if (slideIndex === slides.length) {
        prevBtn.style.display = "inline-block";
        nextBtn.style.display = "none";
    } else {
        prevBtn.style.display = "inline-block";
        nextBtn.style.display = "inline-block";
    }
}

function checkAnswers(chapterId, episodeId) {
    var checkedAnswers = document.querySelectorAll('input[type="radio"]:checked');
    let correctAnswers = []
    if (Array.isArray(answers)) {
        console.log(answers)
    }
    
    answers.forEach(answer => {
        // Iterera genom alla checkedAnswers
        checkedAnswers.forEach(checkedAnswer => {
           
            if (answer.Id == checkedAnswer.value && answer.IsCorrect == true) {
                correctAnswers.push(answer);
            }
        });
    });

    window.sessionStorage.setItem('correctAnswers', correctAnswers.length)
    const fullUrl = `${url}/${chapterId}/${episodeId}`;
    location.href = fullUrl;
}

function checkFinalAnswers() {
    console.log("Knappen fungerar!");
    var checkedAnswers = document.querySelectorAll('input[type="radio"]:checked');
    let correctAnswers = [];

    if (Array.isArray(answers)) {
        console.log(answers);
    }

    answers.forEach(answer => {
        checkedAnswers.forEach(checkedAnswer => {
            if (answer.Id == checkedAnswer.value && answer.IsCorrect === true) {
                correctAnswers.push(answer);
            }
        });
    });

    const correctCount = correctAnswers.length;
    const totalQuestions = document.querySelectorAll('.question-block').length;

    // Spara till sessionStorage så vi kan hämta i resultatsidan om vi vill
    window.sessionStorage.setItem('correctFinalAnswers', correctCount);
    window.sessionStorage.setItem('totalQuestions', totalQuestions);

    // Vidare till resultatsidan för slutprov
    location.href = "/Test/ExaminationResult";
}



