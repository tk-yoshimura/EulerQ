import matplotlib.pyplot as plt
import matplotlib.cm as cm

def make_polar():
    plt.clf()
    plt.figure(figsize = (12, 10)) 

    ax = plt.subplot(111, polar=True)
    ax.set_xticklabels(["$0$", "$\pi/4$", "$\pi/2$", "$3\pi/4$", "$\pm \pi$", "$-3\pi/4$", "$-\pi/2$", "$-\pi/4$"])

    ax.set_rlim([0, 1])
    ax.set_rticks([])
    
    return ax