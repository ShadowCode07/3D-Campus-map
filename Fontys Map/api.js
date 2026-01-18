let viewer = null;

const apiBaseUrl =
  "https://campusmapapi-fkb7azh6h9hah4b9.germanywestcentral-01.azurewebsites.net/api";

function byId(id) {
  return document.getElementById(id);
}

function setLoading(text) {
  const node = byId("loadingMsg");
  if (node) node.textContent = text;
  console.log("[Tour]", text);
}

function showLoading() {
  byId("loadingPanel")?.classList.remove("hidden");
}

function hideLoading() {
  byId("loadingPanel")?.classList.add("hidden");
}

function openMenu() {
  document.querySelector(".menu")?.classList.add("menuOpen");
}

function closeMenu() {
  document.querySelector(".menu")?.classList.remove("menuOpen");
}

function toggleMenu() {
  const menu = document.querySelector(".menu");
  if (!menu) return;
  menu.classList.contains("menuOpen") ? closeMenu() : openMenu();
}

function bindMenu() {
  byId("menuBtn")?.addEventListener("click", toggleMenu);
  byId("menuCloseBtn")?.addEventListener("click", closeMenu);

  window.addEventListener("keydown", (e) => {
    if (e.key === "Escape") closeMenu();
  });
}

function formatScene(sceneId, sceneObj) {
  if (sceneObj && typeof sceneObj.title === "string" && sceneObj.title.trim()) {
    return sceneObj.title.trim();
  }
  return sceneId
    .replace(/_/g, " ")
    .replace(/([a-z])([A-Z])/g, "$1 $2")
    .replace(/(\D)(\d+)/g, "$1 $2")
    .trim();
}

function renderScenes(config) {
  const container = byId("sceneNav");
  if (!container) return;

  const scenes = config?.scenes || {};
  const ids = Object.keys(scenes);

  container.innerHTML = "";

  for (const id of ids) {
    const btn = document.createElement("button");
    btn.type = "button";
    btn.className = "sceneBtn";
    btn.dataset.sceneId = id;

    const name = formatScene(id, scenes[id]);

    btn.innerHTML = `
      <div class="sceneName">${name}</div>
      <div class="sceneId">${id}</div>
    `;

    btn.addEventListener("click", () => {
      if (!viewer) return;
      viewer.loadScene(id);
      closeMenu();
    });

    container.appendChild(btn);
  }
}

function setActiveScene(sceneId, config) {
  document.querySelectorAll(".sceneBtn").forEach((b) => {
    b.classList.toggle("sceneActive", b.dataset.sceneId === sceneId);
  });

  const scenes = config?.scenes || {};
  const title = formatScene(sceneId, scenes[sceneId]);
  if (byId("sceneTitle")) byId("sceneTitle").textContent = title;
}

function bindSearch() {
  const input = byId("searchInput");
  if (!input) return;

  input.addEventListener("input", () => {
    const q = input.value.trim().toLowerCase();
    document.querySelectorAll(".sceneBtn").forEach((b) => {
      const text = b.textContent.toLowerCase();
      b.style.display = text.includes(q) ? "" : "none";
    });
  });
}

async function loadTour(buildingId, options = {}) {
  const { firstScene } = options;

  showLoading();
  setLoading("Fetching tour configuration…");

  const qs = firstScene ? `?firstScene=${encodeURIComponent(firstScene)}` : "";
  const url = `${apiBaseUrl}/buildings/${buildingId}/tour/pannellum${qs}`;

  const res = await fetch(url);
  if (!res.ok) {
    setLoading(`Config request failed (HTTP ${res.status})`);
    throw new Error(`Config request failed (HTTP ${res.status})`);
  }

  const config = await res.json();

  config.strings = {
    ...(config.strings || {}),
    loadingLabel: "Loading 360° scene…",
    loadButtonLabel: "Enter tour",
    bylineLabel: "",
  };

  config.default = config.default || {};
  if (firstScene) config.default.firstScene = firstScene;

  setLoading("Building navigation…");
  renderScenes(config);
  bindSearch();

  setLoading("Initializing viewer…");
  if (typeof pannellum === "undefined" || !pannellum.viewer) {
    throw new Error("pannellum.js not loaded. Ensure pannellum.js loads BEFORE api.js.");
  }

  if (viewer) {
    try { viewer.destroy(); } catch (_) {}
    viewer = null;
  }

  viewer = pannellum.viewer("panorama", config);

  viewer.on("error", (err) => {
    console.error("[Tour] Pannellum error:", err);
    setLoading("Panorama failed to load (image path / CORS / 404). Check console.");
  });

  viewer.on("load", () => {
    setLoading("Ready!");
    setTimeout(hideLoading, 200);
    setActiveScene(viewer.getScene(), config);
  });

  viewer.on("scenechange", (sceneId) => {
    showLoading();
    setLoading("Loading next location…");
    setActiveScene(sceneId, config);
  });

  setTimeout(() => hideLoading(), 6000);

  return viewer;
}

document.addEventListener("DOMContentLoaded", () => {
  bindMenu();
  closeMenu();

  loadTour(1).catch((err) => {
    console.error(err);
    showLoading();
    setLoading("Something went wrong. Check console for details.");
  });
});
