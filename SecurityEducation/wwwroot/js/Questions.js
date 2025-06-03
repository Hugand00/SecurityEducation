let slideIndex = 1;
showSlides(slideIndex);


function plusSlides(n) {
    showSlides(slideIndex += n);
}


function plusSlides(n) {
    
    if (n > 0) {
        const currentSlide = document.getElementsByClassName("mySlides")[slideIndex - 1];
        const selected = currentSlide.querySelector('input[type="radio"]:checked');
        if (!selected) {
            alert("Du måste välja ett svar innan du går vidare.");
            return; 
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

   
    if (n > slides.length) { slideIndex = slides.length; }
    else if (n < 1) { slideIndex = 1; }
    else { slideIndex = n; }

    
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

   
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";

    
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


    window.sessionStorage.setItem('correctFinalAnswers', correctCount);
    window.sessionStorage.setItem('totalQuestions', totalQuestions);

    
    location.href = "/Test/ExaminationResult";
}



