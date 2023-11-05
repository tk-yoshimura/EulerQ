import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

data = pd.read_csv("../results/euler_q_n32.csv", encoding="utf-8")

x, y, lny, v = data["x"], data["y:=euler_q(x)"], data["lny:=ln(y)"], data["v:=lny*(1-x^2)"]

# plot regular graph
plt.clf()

plt.figure(figsize=(8, 6))

plt.plot(x, y)
plt.xlim([-1, 1])
plt.ylim([0, 1.4])

plt.xlabel("$q$")
plt.ylabel("$(q; q)_\infty$")

plt.grid()

plt.savefig("../figures/euler_q_plot.svg")

# plot log graph
plt.clf()

plt.figure(figsize=(8, 6))

lny = lny[1:-1].astype(np.float32)

plt.plot(x[1:-1], lny)
plt.xlim([-1, 1])
plt.ylim([-10, 1])

plt.xlabel("$q$")
plt.ylabel("$\log ((q; q)_\infty )$")

plt.grid()

plt.savefig("../figures/euler_q_logplot.svg")


# plot v plot
plt.clf()

plt.figure(figsize=(8, 6))

root = "-0.6793343699899927"

plt.plot([-0.25, 0.25], [0.25, -0.25], color="gray", linestyle="dashed")
plt.plot([float(root), float(root)], [0, -0.15], color="gray", linestyle="dashed")

plt.plot(x, v)
plt.xlim([-1, 1])
plt.ylim([-3.5, 0.5])

plt.xlabel("$q$")
plt.ylabel("$\log ((q; q)_\infty ) \cdot (1-q^2)$")


plt.scatter([-1, float(root), 1], [-(np.pi**2) / 12, 0, -(np.pi**2) / 3])
plt.text(-1, -(np.pi**2) / 12, "  $- \pi^2 / 12$")
plt.text(1, -(np.pi**2) / 3, "$- \pi^2 / 3$  ", ha="right")

plt.text(0.25, -0.25, "  $-q$")

plt.text(float(root), -0.25, root + "...", ha="left")

plt.grid()

plt.savefig("../figures/euler_q_vplot.svg")


# plot r plot
plt.clf()

plt.figure(figsize=(8, 6))

r = (v + x) / (x * x)

plt.plot(x, r)
plt.plot(x, r * 1.025, color="gray", linestyle="dashed")
plt.xlim([-1, 1])
plt.ylim([-2.5, -1.25])
plt.yticks(np.arange(-20, -9) / 8)

plt.xlabel("$q$")
plt.ylabel("$( \log ((q; q)_\infty ) \cdot (1-q^2) + q) / q^2 $")

plt.scatter([0], [-1.5])
plt.text(0, -1.5, " exact: -3/2", va="bottom")

plt.grid()

plt.savefig("../figures/euler_q_rplot.svg")