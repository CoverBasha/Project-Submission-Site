const container = document.getElementById("my-container");
const registerBtn = document.getElementById("register");
const loginBtn = document.getElementById("login");

registerBtn.addEventListener('click', ()=>{
    container.classList.add("active");
    console.log("sign up clicked");
    
})

loginBtn.addEventListener('click', ()=>{
    container.classList.remove("active");
});