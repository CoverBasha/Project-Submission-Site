const container = document.getElementById("my-container");
const registerBtn = document.getElementById("register");
const loginBtn = document.getElementById("login");
const signupSubmit = document.getElementById("signup-submit");
const otpShadow = document.querySelector(".shadow-background");
const otpPopup = document.querySelector(".otp-popup");
const otpInput = document.getElementById("otp-input");
const otpSubmit = document.getElementById("otp-submit");
const otpForm = document.querySelector(".otp-card");

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
    console.log("sign up clicked");

});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});

otpShadow.addEventListener('click', () => {
    otpPopup.classList.add("hidden");
});

otpInput.addEventListener('input', () => {
    otpInput.value = otpInput.value.replace(/[^0-9]/g, '');
    if (otpInput.value.length == 4) {
        console.log("testing");

        otpSubmit.removeAttribute('disabled');
        otpSubmit.style.backgroundColor = 'rgba(158, 212, 192, 1)';
        otpSubmit.style.cursor = 'pointer';
    } else {
        otpSubmit.style.backgroundColor = 'rgba(158, 212, 192, 0.4)';
        otpSubmit.style.cursor = 'not-allowed';
        otpSubmit.setAttribute('disabled', true);
    }
})
