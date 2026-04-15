document.addEventListener("DOMContentLoaded", function () {
  const content = document.getElementById("novel-content");

  if (!content) return; // กันพัง

  let fontSize = localStorage.getItem("fontSize") || "font-medium";
  let fontFamily = localStorage.getItem("fontFamily") || "font-sarabun";

  content.classList.add(fontSize, fontFamily);

  document.getElementById("font-increase").onclick = () => {
    content.classList.remove("font-small", "font-medium", "font-large");

    if (fontSize === "font-small") fontSize = "font-medium";
    else if (fontSize === "font-medium") fontSize = "font-large";

    content.classList.add(fontSize);
    localStorage.setItem("fontSize", fontSize);
  };

  document.getElementById("font-decrease").onclick = () => {
    content.classList.remove("font-small", "font-medium", "font-large");

    if (fontSize === "font-large") fontSize = "font-medium";
    else if (fontSize === "font-medium") fontSize = "font-small";

    content.classList.add(fontSize);
    localStorage.setItem("fontSize", fontSize);
  };

  document.getElementById("font-family").onchange = (e) => {
    content.classList.remove("font-sarabun", "font-prompt");

    fontFamily = "font-" + e.target.value;
    content.classList.add(fontFamily);

    localStorage.setItem("fontFamily", fontFamily);
  };
});
