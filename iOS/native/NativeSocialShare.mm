#import "NativeSocialShare.h"  
#import <Social/Social.h>  
#import "IosTool.h"  
  
@implementation NativeSocialShare  

+(void) nativeShare:(NSString *)text media:(NSString *)media {  
    NSLog(@"==============>nativeShare");  

    NSData *imageData = [[NSData alloc]initWithBase64EncodedString:media options:0];  
    UIImage *image = [[UIImage alloc] initWithData:imageData];  
          
    UIActivityViewController *controller = [[UIActivityViewController alloc] initWithActivityItems:@[image] applicationActivities:nil];  
      
    UIViewController *vc =  UnityGetGLViewController();  
      
    NSArray *vComp = [[UIDevice currentDevice].systemVersion componentsSeparatedByString:@"."];  
    if ([[vComp objectAtIndex:0] intValue] >= 8) {  
        UIPopoverPresentationController *presentationController = [controller popoverPresentationController];  
        presentationController.sourceView = vc.view;  
        presentationController.sourceRect = CGRectMake(vc.view.bounds.origin.x+vc.view.bounds.size.width/2,  
                                                       vc.view.bounds.origin.y+vc.view.bounds.size.height,  
                                                       0, 0);  
        presentationController.permittedArrowDirections = 0;  
    }  
      
    [vc presentViewController:controller animated:YES completion:nil];  
  
    controller.completionWithItemsHandler = ^(UIActivityType  _Nullable activityType, BOOL completed, NSArray * _Nullable returnedItems, NSError * _Nullable activityError) {  
//        NSLog(@"activityType :%@", activityType);  
        if (completed){  
            UnitySendMessage("MainGameObject", "NativeShareSuccess", [IosTool NSStringToChar:activityType]);  
//            NSLog(@"completed!");  
        }else{  
            if (activityType != nil){  
                UnitySendMessage("MainGameObject", "NativeShareCancel", [IosTool NSStringToChar:activityType]);  
            }else{  
                UnitySendMessage("MainGameObject", "NativeShareCancel", [IosTool NSStringToChar:@""]);  
            }  
//            NSLog(@"cancel!");  
        }  
    };  
}  
  
@end  
  