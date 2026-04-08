const savedTheme = localStorage.getItem("theme") || "light";
document.documentElement.setAttribute("data-bs-theme", savedTheme);

function toggleTheme() {
  const current = document.documentElement.getAttribute("data-bs-theme");
  const next = current === "dark" ? "light" : "dark";
  document.documentElement.setAttribute("data-bs-theme", next);
  localStorage.setItem("theme", next);

  const sw = document.getElementById("darkModeSwitch");
  if (sw) sw.checked = next === "dark";
}

document.addEventListener("DOMContentLoaded", () => {
  const sw = document.getElementById("darkModeSwitch");
  if (sw) sw.checked = savedTheme === "dark";
});
