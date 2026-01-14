let viewer = null;
const API_BASE_URL = "https://campusmapapi-fkb7azh6h9hah4b9.germanywestcentral-01.azurewebsites.net/api";

export async function loadTour(buildingId, { firstScene } = {}) {
  const qs = firstScene ? `?firstScene=${encodeURIComponent(firstScene)}` : "";
  const res = await fetch(`${API_BASE_URL}/buildings/${buildingId}/tour/pannellum${qs}`);
  if (!res.ok) {
    throw new Error(`Failed to load tour: ${res.status}`);
  }

  const config = await res.json();

  if (viewer)
  {
    viewer.destroy();
    viewer = null;
  }

  viewer = pannellum.viewer("panorama", config);
  return viewer;
}

loadTour(1).catch(console.error);