import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cm as cm

def make_polar():
    plt.clf()
    plt.figure(figsize = (12, 10)) 

    ax = plt.subplot(111, polar=True)
    ax.set_xticklabels([r"$0$", r"$\pi/4$", r"$\pi/2$", r"$3\pi/4$", r"$\pm \pi$", r"$-3\pi/4$", r"$-\pi/2$", r"$-\pi/4$"])

    ax.set_rlim([0, 1])
    ax.set_rticks([])
    
    return ax