const PROXY_CONFIG = [
  {
    context: [
      "/location",
      "/currentweather",
      "/dailyforecast"
    ],
    target: "http://localhost:5042",
    secure: false
  }
]

module.exports = PROXY_CONFIG;

