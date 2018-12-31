#import <UIKit/UIKit.h>  
  
@interface NativeSocialShare : NSObject  
  
+ (void) nativeShare:(NSString*)text media: (NSString*) media;  
+ (void) nativeShareUrl:(NSString *)url;  
  
@end  
