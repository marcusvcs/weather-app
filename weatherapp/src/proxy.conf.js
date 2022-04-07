const PROXY_CONFIG = [
  {
    context: [
      "/location",
    ],
    target: "http://localhost:5042",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
